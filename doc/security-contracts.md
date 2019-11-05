# Security Contracts

A [Security contract](./glossary.md#security-contract) is a declarative YAML document that allows idempotent definition of a desired [A3S](https://github.com/GrindrodBank/A3S) state. Almost all aspects of [A3S](https://github.com/GrindrodBank/A3S) can be defined using security contracts. This document details each aspect of a [security contract](./glossary.md#security-contract) , using the [A3S quickstart reference secrity contract](./a3s-security-contract.yaml), which [A3S](https://github.com/GrindrodBank/A3S) uses to configure a known state within the [quickstart guide](./../quickstart/README.md), as a practical reference. 

# Security Contract Components

There are three main sections defined within a [security contract](./a3s-security-contract.yaml). **Note!** Each section of the [security contract](./glossary.md#application) can be supplied as a valid request body within the `SecurityContract` API! It is not required to supply all three components when applying a [security contract](./glossary.md#security-contract) via the API. This allows each section to be applied independently from others, and enables different components running in the environment to be responsible for supplying the component of the [security contract](./glossary.md#security-contract) that is relevant to it. For example, in a production environment, an [application](./glossary.md#application) would apply it's own `applications` section of the contract on deployment in order to declare the latest state of it's [permissions](./glossary.md#permission) and [data policies](./glossary.md#data-policy).

The three main components of a [security contract](./glossary.md#security-contract) are listed below. **Note!** Each section is defined as a list with the [security contract](./glossary.md#security-contract), and enables defining multiple definitions within it. For example, the `applications` section allows defining multiple [application](./glossary.md#application).

* `applications` - This is where [applications](./glossary.md#application) are defined. An [application](./glossary.md#application) defines itself, it's [permissions](./glossary.md#permission), and it's [data policies](./glossary.md#data-policy). 
* `clients` - This is where OAuth 2.0 [clients](./glossary.md#client) are registered.
* `defaultConfigurations` - This enables defining conifguration state of [A3S](https://github.com/GrindrodBank/A3S). It can be used to create the following configurations and inter-relate them:
  * [Functions](./glossary.md#function) - Create [functions](./glossary.md#function) from [permissions](./glossary.md#permission) that have been defined.
  * [Roles](./glossary.md#role) - Create [roles](./glossary.md#role) from [functions](./glossary.md#function).
  * [Users](./glossary.md#user) - Create [users](./glossary.md#user) and assign [roles](./glossary.md#role) to them.
  * [LDAP Authentication Modes](./glossary.md#ldap-authentication-mode) - Define an [LDAP Authentication Modes](./glossary.md#ldap-authentication-mode) for associating [users](./glossary.md#user) with.
  * [Teams](./glossary.md#team) - Create [teams]./glossary.md#team) and assign [child teams](./glossary.md#child-team) to them.

Each section, and it's possible attributes are dicussed in detail in the following sections of this document.

## Applications

* `applications` - The top level element for defining [applications](./glossary.md#application). It is defined as a list, as multiple [applications](./glossary.md#application) can be defined within a single [security contract](./glossary.md#security-contract).
* `fullname` - The fullname of the [application](./glossary.md#application). **Note!** Currently, the `fullname` of the [application](./glossary.md#application) is used as the scope and audience values within [JWT's](./glossary.md#jwt) issued by [A3S](https://github.com/GrindrodBank/A3S). This requires the `fullname` value to be unique, lower case, and contain no spaces.
* `applicationFunctions` - These are developer defined groups of related permissions. **Note!** The `applicationFunction` entity's only purpose is to make long lists of [permissions](./glossary.md#permission) more human readable within [security contract](./glossary.md#security-contract) definitions! They are utterly distinct from, and not to be confused with [functions](./glossary.md#function).
* `permissions`- This is the list of related [permissions](./glossary.md#permission) contained within an `applicationFunction`. **Note!** Each [permission](./glossary.md#permission) name must be unique across the entire enterprise (all [applications](./glossary.md#application)), which is why a `<application>.<applicationFunction>.<permissionName>` naming convention is highly recommended! If any [permission](./glossary.md#permission) is found to already exist within [A3S](https://github.com/GrindrodBank/A3S) (potentially defined by another [application](./glossary.md#application)), the entire [security contract](./glossary.md#security-contract) will be rejected. A `description` should be defined for each [permission](./glossary.md#permission) for the purpose of giving context to [A3S](https://github.com/GrindrodBank/A3S) users that are adminstering them.
* `dataPolicies` - Enables defining [data policies](./glossary.md#data-policy) for the [application](./glossary.md#application), which are policies that modify the behaviour of returned data that a particular resource could return. Using the `a3s.viewYourTeamsOnly` [data policy](./glossary.md#data-policy), defined in the [A3S quickstart reference secrity contract](./a3s-security-contract.yaml), as an example. When fetching a list of teams from the [A3S](https://github.com/GrindrodBank/A3S) API, if this [data policy](./glossary.md#data-policy) is present within the [JWT](./glossary.md#jwt), then only the teams that the accessing user is a member of will be returned. The absence of the `a3s.viewYourTeamsOnly` `dataPolicy` results in all the teams (regardless of whether the user is a member of the team or not) being returned. 


## Clients

* `clientId` - The unique ID of the client being defined. This is the value that is used for the `client_id` field when requesting [access tokesn](./glossary.md#access-token) for this client.
* `name` - A human readable/display name for the [client](./glossary.md#client).
* `allowedGrantTypes` - These are the list of [OAuth 2.0 grant types](https://oauth.net/2/grant-types/) the client will support.
* `redirectUris` - Some of the [OAuth 2.0 grant types](https://oauth.net/2/grant-types/) require a URL for the [user](#user) to be redirected back to. These URLs are configured here for the client.
* `postLogoutRedirectUris` - Configures URLs that a [user](./glossary.md#user) using the [client](./glossary.md#client) will be redirected to after logging out.
* `allowedCorsOrigins` - Configure allowed [CORS](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) origins for the [client](./glossary.md#client).
* `allowedScopes` - Configures the [scopes](./glossary.md#scope) that the [client](./glossary.md#client) is permitted to request. **Note!** This is a very important configuration. 
  * In order to access an [application](./glossary.md#application) using the [client](./glossary.md#client), the [application's](./glossary.md#application) name must be set as an allowed [scope](./glossary.md#scope).
  * `openid` and `profile` are standard scopes that are required to be configured for the [client](./glossary.md#client) if [ID tokens](./glossary.md#id-token) are to be issued for the [client](./glossary.md#client).
* `clientSecrets` - A list of secrets that are configured for the client. Several of the [OAuth 2.0 grant types](https://oauth.net/2/grant-types/) require specifying a `client_secret` as part of the request to get [access tokens](./glossary.md#access-token).
* `allowedOfflineAccess` - This boolean flag configures whether the [client](./glossary.md#client) is permitted to request [refresh tokens](https://oauth.net/2/grant-types/refresh-token/).

## Default Configurations

`name` - A human readable name for the default configuration. This attribute is intended purely for increasing human readability of the document. It has no bearing on any of the resulting configurations. The indetation of the following bullet points is indented to follow the indetation heirachy of the `defaultConfigurations` section of a [security contract](./glossary.md#security-contract).

`applications` - [Functions](./glossary.md#function) and the [permissions](./glossary.md#permission) that they are comprised of must all belong to a single [application](./glossary.md#function). Much like the [Applications](#applications) section of a [security contract](./glossary.md#security-contract), these are configured under a parent `application` section assert that this occurs correctly.
  * `name` - The name of the [application](./glossary.md#permission) to create the `functions` for.
  * `functions` - The list of [functions](./glossary.md#function) that is to be created for the [application](./glossary.md#application).
    * `name` - A human readable/business friendly name for the [function](./glossary.md#function) to be created. 
    * `description` - A brief description of the [function](./glossary.md#function). Recall, [functions](./glossary.md#function) intended to abstract more technical [application](./glossary.md#application) [permissions](./glossary.md#permission) and present them as more business understandable [functions](./glossary.md#function). The description can go a long way to achieving this.
    * `permissions` - The list of [application](./glossary.md#application) [permissions](./glossary.md#permission) to assign to the [function](./glossary.md#function). **Note!** The [permissions](./glossary.md#permission) defined here must be defined within an [application](./glossary.md#application) in the [Applications](#applications) section of the [security contract](./glossary.md#security-contract) for an [application](./glossary.md#application) that has the same `name` as the `applications`->`name` defined within the `DefaultConfigurations` section of the [security contract](./glossary.md#security-contract).

`roles` - A section for definining of [roles](./glossary.md#role). [Roles](./glossary.md#role) can contain [functions](./glossary.md#function) defined in any [application](./glossary.md#application). 
  * `name` - The name of the [role](./glossary.md#role) to create. **Note!** This must be unique across all [applications](./glossary.md#application) secured by [A3S](https://github.com/GrindrodBank/A3S). Recall, many [applications](./glossary.md#application) are able to supply their own `defaultConfigurations` components during deployment. If two [applications](./glossary.md#application) defined a [role](./glossary.md#role) called `Admin`, they will overwrite one another on deployment. **Recall!** [Security contracts](./glossary.md#security-contract) are declarative and idempotent, and supplying the same [role](./glossary.md#role) name, with different `functions` section, will be interpretted as a declarative update of that [role's](./glossary.md#role) [functions](./glossary.md#function).
  * `functions` - The list of [functions](./glossary.md#function) to assign to the [role](./glossary.md#role).

* `ldapAuthenticationModes` - A section for declaring [LDAP authentication modes](./glossary.md#ldap-authentication-mode).
  * `name` - A human readable name for the [LDAP authentication mode](./glossary.md#ldap-authentication-mode).
  * `hostname` - The hostname that will be used for all connections to the [LDAP authentication mode](./glossary.md#ldap-authentication-mode).
  * `port` - The port that will be used for all connections to the [LDAP authentication mode](./glossary.md#ldap-authentication-mode).
  * `isLdaps` - A flag toggling whether to connect to the [LDAP authentication mode](./glossary.md#ldap-authentication-mode) using the LDAP secure protocol.
  * `account` - [A3S](https://github.com/GrindrodBank/A3S) must use a valid account when connecting to the [LDAP authentication mode](./glossary.md#ldap-authentication-mode). This is the name of the account that A3S](https://github.com/GrindrodBank/A3S) will use when connecting to the [LDAP authentication mode](./glossary.md#ldap-authentication-mode).
  * `baseDn` - The `baseDn` to use for all lookups.
  * `ldapAttributes` - [A3S](https://github.com/GrindrodBank/A3S) writes verified [user claims](./glossary.md#user-claim) into [JWTs](./glossary.md#jwt) issued to [users](./glossary.md#user). When federating [users](./glossary.md#user) from [LDAP authentication modes](./glossary.md#ldap-authentication-mode) the required [user](./glossary.md#user) attibutes may need to be mapped from how they are stored within the target [LDAP authentication mode](./glossary.md#ldap-authentication-mode). The `ldapAttributes` field provides configuration for how to perform this mapping, using lists of key-value mappings, defined below.
    * `userField` - This is the [A3S](https://github.com/GrindrodBank/A3S) [user](./glossary.md#user) field that will be mapped onto from the [LDAP authentication mode](./glossary.md#ldap-authentication-mode).
    * `ldapField` - This the [user](./glossary.md#user) within the [LDAP authentication mode](./glossary.md#ldap-authentication-mode) that will be mapped onto the `userField` within the [A3S](https://github.com/GrindrodBank/A3S) [user](./glossary.md#user) store.
  
* `users` - A section for defining [users](./glossary.md#user)
  * `username` - Allows defining of a [user's](./glossary.md#user) username. This value must be unique and is used as a key for identifying a [user](./glossary.md#user). **Note!** Owing to the idempotent nature of the [security contracts](./glossary.md#security-contract), any attributes supplied for a user identified by the same `username` will be treated as the same [user](./glossary.md#user). Therefore, if two [applications](./glossary.md#application) define the a [user](./glossary.md#user) with the same `username`, they will overwrite one another's state when the contract is applied!
  * `name` - The first names of the [user](./glossary.md#user).
  * `surname` - The surname of the [user](./glossary.md#user).
  * `email` - The [user's](./glossary.md#user) email address.
  * `password` - The [user's](./glossary.md#user) **plain text** password. This allows for the creation of user's using a plain text password. it is insecure to store the security contract with the plain text passwords of [users](./glossary.md#user) within it. However, sometimes this is required, so this fields is optional.
  * `hashedPassword` - This field allows for configuring the [user's](./glossary.md#user) password using the hashed and salted form.
  * `avatar` - This field enables setting a [user's](./glossary.md#user) avatar. This is a base64 encoded byte representation of the avatar image. It is optional.
  * `roles` - This is a list of [roles](./glossary.md#role) that are to be assinged to the [user](./glossary.md#user). The [role](./glossary.md#role) `name` is used to refer to a [role](./glossary.md#role) within the list.

* `teams` - A section for defining [teams](./glossary.md#team).
  * `name` - The name of the [team](./glossary.md#team). This value must be unique and is used as a key for identifying a [team](./glossary.md#team). **Note!** Owing to the idempotent nature of the [security contracts](./glossary.md#security-contract), any attributes supplied for a [team](./glossary.md#team) identified by the same `name`, will result in this attributes being applied to that [team](./glossary.md#team). This is an important consideration when defining teams thay may be declared by an [application](./glossary.md#application) on deployment.
  * `description` - A simple description of the team.
  * `users` - A list of [users](./glossary.md#user) to place into the team. The `username` of the [user](./glossary.md#user) is used here.
  * `teams` - A list of [child teams](./glossary.md#child-team) to assign to the defined [team](./glossary.md#team). Note, see the details about creating compound teams [here](./glossary.md#team). 
