# Using License Rat

These functions pertain to maven wrapper operations for checking, generating and removing the license headers that appear at the top of all source code files in the repository.

The license header that is applied is sourced from the `/doc/license/copyright_header.txt` template.

From <http://code.mycila.com/license-maven-plugin/>

## Commands

In order to check whether all the files have generated copyright headers:

```bash
./mvnw -f pom-rat.xml license:check
```

In order to generated all copyright headers for supported file types:

```bash
./mvnw -f pom-rat.xml license:format
```

In order to remove all the generated copyright headers from files:

```bash
./mvnw -f pom-rat.xml license:remove
```
