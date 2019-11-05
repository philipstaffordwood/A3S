//
// *************************************************
// Copyright (c) 2019, Grindrod Bank Limited
// License MIT: https://opensource.org/licenses/MIT
// **************************************************
//

// Jenkinsfile for upstream A3S build pipeline

// Parameters
def codeName = "a3s"
def label = "a3s-build-${UUID.randomUUID().toString()}"
def namespace = "jenkins"

// Environment variables used:
// DOCKER_REPO
// SONAR_HOST_URL
// SONAR_AUTH_TOKEN
// JFROG_USERNAME
// JFROG_PASSWORD

//This is the kubernetes pod used to execute the pipeline:
podTemplate(label: label, containers: [
  containerTemplate(name: 'docker', image: 'docker', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'dotnet-sdk', image: 'nosinovacao/dotnet-sonar', command: 'cat', ttyEnabled: true),
  //jfrog-cli - for deleting ephemeral dockerfile stored on artifactory during build
  containerTemplate(name: 'jfrog-cli', image: 'docker.bintray.io/jfrog/jfrog-cli-go:latest', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'sonar-scanner', image: 'newtmitch/sonar-scanner:3.2.0', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'openapi-generator', image: 'openapitools/openapi-generator-cli:latest', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'node', image: 'node:10.13.0-stretch', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'maven', image: 'maven:3.5.3-jdk-8-alpine', command: 'cat', ttyEnabled: true),
],
volumes: [
  hostPathVolume(mountPath: '/var/run/docker.sock', hostPath: '/var/run/docker.sock'),
  hostPathVolume(mountPath: '/tmp', hostPath: "/tmp/${label}") 
],
imagePullSecrets: ['baobab-artifactory-registry']) {

node(label) {
    def myRepo = checkout scm
    def gitCommit = myRepo.GIT_COMMIT
    def gitBranch = myRepo.GIT_BRANCH
    def shortGitCommit = "${gitCommit[0..10]}"
    def localRepository = "${codeName}"
    def remoteRepository = "${DOCKER_REPO}${codeName}"
    def localRepoIdServer = "identityserver"
    def remoteRepositoryIdServer = "${DOCKER_REPO}${localRepoIdServer}"
    def baseImageName = "${codeName}"
    def deploymentName = "${codeName}-${shortGitCommit}"
	def mainRepoBranch = 'master'

    try {
            stage('Unit tests') {
               container('dotnet-sdk') {
                   sh """
                       dotnet --version                 
                       dotnet restore A3Service.sln
                       dotnet build A3Service.sln
                       dotnet test A3Service.sln --no-build
                   """
               }
            }

            stage('License Header Check') {
                container('maven') {
                    sh "mvn clean -f pom-rat.xml license:check"
                }
            }

            // FIRST WE DO STATIC CODE ANALYSIS USING SONARQUBE'
            // This works but fails with a 403- entity too large error when submitting the report.
            stage('SonarQube Analysis') {
                container('dotnet-sdk') {
                    withSonarQubeEnv('sonarqube-cluster') {
                        sh """
                            dotnet --version
                            java -version
                            dotnet --list-runtimes
                            export PATH="$PATH:/home/jenkins/.dotnet/tools"
                            dotnet tool install --global dotnet-sonarscanner
                            dotnet test A3Service.sln /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
                            dotnet sonarscanner begin /n:${baseImageName} /key:${baseImageName} \
                                /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.login=${SONAR_AUTH_TOKEN} \
                                /d:sonar.exclusions=**/Data/**,**/Migrations/**,**/Quickstart/**,**/wwwroot/**,**/A3SApiResources/**,**/AbstractApiControllers/**,**/Models/**,**/Repositories/** \
                                /d:sonar.test.inclusions=tests/**/* \
                                /d:sonar.coverage.exclusions=**/*Context.cs,**/Program.cs,**/Startup.cs,**/Config.cs,**/A3SApiResources/**,**/AbstractApiControllers/**,**/Models/**,**/Repositories/** \
                                /d:sonar.sourceEncoding=UTF-8 \
                                /d:sonar.cs.opencover.reportsPaths=tests/za.co.grindrodbank.a3s.tests/coverage.opencover.xml,tests/za.co.grindrodbank.a3s-identity-server.tests/coverage.opencover.xml
                            dotnet build A3Service.sln
                            dotnet sonarscanner end /d:sonar.login=${SONAR_AUTH_TOKEN}
                        """
                    }
                }
            }

            //NEXT WE BUILD IN THE 'docker' CONTAINER IN THE 'Build Docker Image'
            //    the full build logic in embedded in a Dockerfile build
            stage('Build Docker A3S Image') {
                container('docker') {
                    sh "docker --version"
                    sh "docker build -t ${localRepository}:${shortGitCommit} -f a3s-Dockerfile ."
                }
            }
    	
            //WE ONLY PUSH THE DOCKER IMAGE TO ARTIFACTORY 
            //    if we are on the master branch we  push to the latest tag
            stage('Push A3S to Image Repository') {
                container('docker') {
                    withDockerRegistry([credentialsId: 'Jenkins-Artifactory-Credentials', url: "https://${DOCKER_REPO}"]) {
                        sh "docker tag ${localRepository}:${shortGitCommit} ${remoteRepository}:${shortGitCommit}"
                        sh "docker push ${remoteRepository}:${shortGitCommit}"
                        if (gitBranch == mainRepoBranch) {
                            sh "docker tag ${localRepository}:${shortGitCommit} ${remoteRepository}:latest"
                            sh "docker push ${remoteRepository}:latest"
                        }
                    }
                }
            }

            stage('Build Docker IdentityServer Image') {
                container('docker') {
                    sh "docker --version"
                    sh "docker build -t ${localRepoIdServer}:${shortGitCommit} -f a3s-identity-server-Dockerfile ."
                }
            }
        
            //WE ONLY PUSH THE DOCKER IMAGE TO ARTIFACTORY 
            //    if we are on the master branch we  push to the latest tag
            stage('Push IdentityServer to Image Repository') {
                container('docker') {
                    withDockerRegistry([credentialsId: 'Jenkins-Artifactory-Credentials', url: "https://${DOCKER_REPO}"]) {
                        sh "docker tag ${localRepoIdServer}:${shortGitCommit} ${remoteRepositoryIdServer}:${shortGitCommit}"
                        sh "docker push ${remoteRepositoryIdServer}:${shortGitCommit}"
                        if (gitBranch == mainRepoBranch) {
                            sh "docker tag ${localRepoIdServer}:${shortGitCommit} ${remoteRepositoryIdServer}:latest"
                            sh "docker push ${remoteRepositoryIdServer}:latest"
                        }
                    }
                }
            }

            stage('Generate Typescript Axios Client From OAS3 Spec') {
                container('openapi-generator') {
                    if(gitBranch == "master"){
                      sh "mkdir -p /tmp/$label/a3s-typescript-axios"
                      sh """
                          /usr/local/bin/docker-entrypoint.sh generate  \
                          -i ./doc/a3s-openapi.yaml \
                          -g typescript-axios -o /tmp/$label/a3s-typescript-axios \
                          --model-package=model \
                          --api-package=api \
                          --additional-properties=withSeparateModelsAndApi=true,modelPropertyNaming=camelCase,npmName=@grindrodbank/a3s-api
                       """
                    }else{
                        sh "echo Not generating Axios client for non-master branch builds."
                    }
                }
            }

            stage('Push Typescript Axios Client to Artifactory NPM') {
                def npmrc = ""
                container('docker') {
                    if(gitBranch == "master"){
                        sh "rm -f ~/.npmrc"
                        withCredentials([usernamePassword(credentialsId: 'Jenkins-Artifactory-Credentials', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                            def cmd = "docker run " +
                                    "-e NPM_USER=$USERNAME " +
                                    "-e NPM_PASS='$PASSWORD' " +
                                    "-e NPM_EMAIL=mvniekerk@gmail.com " +
                                    "-e NPM_REGISTRY=https://${ARTIFACTORY_NPM_LOCAL} " +
                                    "-e NPM_SCOPE=@grindrodbank " +
                                    "bravissimolabs/generate-npm-authtoken > ~/.npmrc"
        
                            npmrc = sh(
                                    script: cmd,
                                    returnStdout: true
                            ).trim()
        
                            sh "echo '//${ARTIFACTORY_NPM_LOCAL}:always-auth=true' >> ~/.npmrc"
                        }
                    }else{
                        sh "echo Not generating NPM push credentials for non-master branch builds."
                    }
                }

                container('node') {
                    if(gitBranch == "master"){
                        sh "cd /tmp/$label/a3s-typescript-axios && echo 'node_modules/' >> .npmignore && yarn && yarn build && yarn publish"
                    }else{
                        sh "echo Not pushing Axios client to NPM Artifactory for non-master branch builds."
                    }
                }
            }



    } finally {
        // Clean up the temporary folder created for this build.
        container('docker') {
            sh "rm -rf /tmp/${label}"
        }

        // Always remove temporary images pushed to artifactory
        // any of these steps may fail without failing the pipeline ( || true)
        container('jfrog-cli') {
            withCredentials([usernamePassword(credentialsId: 'Jenkins-Artifactory-Credentials', usernameVariable: 'JFROG_USERNAME', passwordVariable: 'JFROG_PASSWORD')]) {
                sh """
                    jfrog --version || true
                    jfrog rt config --url $ARTIFACTORY_URL --interactive=false --user '$JFROG_USERNAME' --password '$JFROG_PASSWORD' artifactory-server || true
                    jfrog rt s baobab-docker-local/${localRepository}/*/manifest.json | grep 'path'|| true
                    jfrog rt delete --quiet baobab-docker-local/${localRepository}/${shortGitCommit} || true
                    jfrog rt s baobab-docker-local/${localRepository}/*/manifest.json | grep 'path' || true
                """
                sh """
                    jfrog --version || true
                    jfrog rt config --url $ARTIFACTORY_URL --interactive=false --user '$JFROG_USERNAME' --password '$JFROG_PASSWORD' artifactory-server || true
                    jfrog rt s baobab-docker-local/${localRepoIdServer}/*/manifest.json | grep 'path'|| true
                    jfrog rt delete --quiet baobab-docker-local/${localRepoIdServer}/${shortGitCommit} || true
                    jfrog rt s baobab-docker-local/${localRepoIdServer}/*/manifest.json | grep 'path' || true
                """
            }//credentials
        }//container
    }//finally
}//podTemplate
}//node
