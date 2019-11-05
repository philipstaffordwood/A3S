# A3S LDAP Setup

One of A3S' top features is the ability to integrate it's user store with an organizations existing user directory. This is achieved with the use of the Lightweight Directory Access Protocol (LDAP), as used by directories like Microsoft Active Directory, OpenLDAP and FreeIPA.

## Instructions

 *Prerequisites*
 
 This guide assumes that A3S has already been set up according to the [integration guide](./doc/integration-guide.md), and that are already authenticated with  a user that has the following built-in A3S [permissions](./glossary.md#permission):

* `a3s.ldapAuthenticationModes.create`
* `a3s.users.create`

### 1. Create a new LDAP Authentication Mode profile

Create a new LDAP Authentication Mode profile by calling the `CreateLdapAuthenticationMode` API method:

```JSON
URL: {{a3s-host}}/authenticationModes/ldap
Method: POST
Body:
{
    "name": "your-ldap-profile-unique-friendly-name",
    "hostName": "localhost",
    "port": 389,
    "isLdaps": true,
    "account": "admin",
    "password": "admin",
    "baseDn": "dc=mydomain,dc=com"
}
```

#### Body properties

|Property|Description|
|--|--|
|Name|A unique friendly name for your LDAP configuration.|
|hostName|The network hostname of the server or container that the LDAP directory is running on.|
|port|The port that your LDAP directory is listening on.|
|isLdaps|Indicates if the connection will use LDAPS for secure and encrypted communication.|
|account|An administrator user account name for the directory.|
|password|The password to the aforementioned administrator account.|
|baseDn|The Base Object Distinguished Name for the directory.|

### 2. Record the new ldapAuthenticationModeId

A successful create in step 1 will return the following response:

```JSON
{
    "uuid": "c1390078-17ea-4625-aa94-a54c03ddcb1d",
    "name": "your-ldap-profile-unique-friendly-name",
    "hostName": "localhost",
    "port": 389,
    "isLdaps": true,
    "account": "admin",
    "password": "admin",
    "baseDn": "dc=mydomain,dc=com"
}
```

Map the returned uuid into the next call's ldapAuthenticationModeId property.

### 3. Create a new User Profile with the new ldapAuthenticationModeId

Update an existing user (or create a new user) by calling the `CreateLdapAuthenticationMode` API method:

```JSON
URL: {{a3s-host}}/users
Method: POST
Body:
{
    "name": "New User First Name One",
    "surname": "New User Surname One",
    "email": "newone@emailadress.local",
    "username": "new-user-name-one",
    "ldapAuthenticationModeId": "{{authentication-mode-guid}}"
}
```

#### Body properties

|Property|Description|
|--|--|
|Name|The first name of the user.|
|surname|The last name of the user.|
|email|The email address of the user.|
|username|The username of the user inside the directory.|
|ldapAuthenticationModeId|The Id of the LDAP Authentication mode as configured inside A3S.|

[Back to Readme](../README.md)