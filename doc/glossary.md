# Glossary of Terms

## Access Token

Access tokens are [JWT's](#jwt) containing [permissions] that consuming [clients](#client) use to make API requests on behalf of a [user](#user). The access token represents the authorization of a specific [client](#client) and [user](#user) to access specific [resources](#resource) on [applications](#application).
Access tokens must be kept confidential in transit and in storage. The only parties that should ever see the access token are the calling [client](#client) itself, the authorization server ([A3S](https://github.com/GrindrodBank/A3S)), and resource server/[application](#application). 

## Application

This is the [A3S](https://github.com/GrindrodBank/A3S) term for referring to a [resource](#resource) server, as commonly referred to within OAuth 2.0 and OpenID Connect contexts. An application is defined as an A3S-protected [resource](#resource) server, which enforces [user](#user) [permissions](#permission) and [data policies](#data-policy), in addition to standard [OAuth 2.0](https://oauth.net/2/) and [OpenID Connect](https://openid.net) [JWT](#jwt) components.

## Application Function

These are developer defined groups of related [permissions](#permission). **Note!** The `applicationFunction` entity's only purpose is to make long lists of [permissions](#permission) more human readable within [security contract](#security-contract) definitions! They are utterly distinct from, and not to be confused with [functions](#function)

## Client

Within the context of [A3S](https://github.com/GrindrodBank/A3S) and it's documentation, the term client refers to the technical client, as commonly referred to within the [OAuth 2.0](https://oauth.net/2/) and [OpenID Connect](https://openid.net) protocols. [OAuth 2.0](https://oauth.net/2/) provides clients "secure delegated access" to server [resources](#resource) on behalf of a [resource](#resource) owner. It specifies a process for [resource](#resource) owners to authorize third-party access to their server [resources](#resource) without sharing their credentials. A [user](#user) is required to use a registered [client](#client) when obtaining [access tokens](#access-token) for accessing [resources](#resource) served by [applications](#application).

## Child Team

A child team is [team](#team) that has been assigned to a another [team](#team). [Teams](#team) can have other [teams](#team) assigned to them, creating a [compound team](#parent-team). A [compound team](#parent-team) is also referred to as a [parent team](#parent-team), as it has one or more [child teams](#child-team) assigned to it.

## Data Policy

Policies that modify the behaviour of returned data that a particular [resource](#resource) could return. These policies differ from [permissions](#permission) in that they are intended to enable data filtering based on [data policy](#data-policy) [user claims](#user-claim) present within the [JWT](#jwt) issued for a [user](#user), rather than restrict access to [resources](#resource). They are defined by [applications](#application) using [security contracts](#security-contract).

* Data Policies are assigned to [teams](#team). 
* [Users](#user) within a [team](#team) inherit any data policies assigned to that team.
* [Child teams](#child-team), and their [users](#user) inherit the data policies of their parent team.
* [Parent teams](#parent-team) and their users **do not** inherit data policies assigned to any of their [child teams](#child-team).

## Effective Permission Set

A [user's](#user) effective [permission](#permission) set is defined as the [permissions](#permission) that will be written into [JWTs](#jwt) issued for the [user](#user). The effective [permission](#permission) set is determined as follows:

1. All the [roles](#role) assigned to the user are obtained.
2. All the [functions](#function) associated with the [roles](#roles) obtained in step 1. are obtained.
3. The distinct union of all the [permissions](#permission) that are assigned to the [functions](#functions) obtained in step 2. are the [user's](#user) effective [permissions](#permission) set.

## Function

A Function is a grouping of [permissions](#permission). Business users assign [permissions](#permission) to [functions](#function) to create abstractions for business capabilities represented by the collection of those [permissions](#permission). **Note!** Functions can only contain permissions originating from a single [application](#application). [Functions](#function) are assigned to [roles](#role). [Roles](#role) are assigned to [users](#user).

## ID Token

ID Tokens follow the [JWT](#jwt) standard, which means that their basic structure conforms to the typical [JWT](#jwt) Structure, and they contain standard [JWT](#jwt) [user claims](#user-claim) asserted about the token itself.

However, beyond what is required for [JWT](#jwt), ID Tokens also contain claims asserted about the authenticated [user](#user), which are pre-defined by the OpenID Connect (OIDC) protocol, and are thus known as standard OIDC claims. Some standard OIDC claims include:

* name
* nickname
* picture
* email
* email_verified

## JWT

Acronym for JSON Web Token. Please refer to [the JWT documentation](https://jwt.io/introduction/) for more details.

## LDAP Authentication Mode

[A3S](https://github.com/GrindrodBank/A3S) supports federating users that are stored within LDAP servers and can authenticate [users](#user) stored within these stores. An LDAP authentication mode models an LDAP user store, it's access credentials, and possible [user claim](#user-claim) mapping attributes, enabling [A3S](https://github.com/GrindrodBank/A3S) to interact with the LDAP server to fulfill these capabilities.

## OAS3

This is an acronym for [OpenAPI Specification - V3](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.0.md).

## Parent Team

[Teams](#team) can also have other [teams](#team) assigned to them, creating a [compound team](#parent-team). A [compound team](#parent-team) is also referred to as a [parent team](#parent-team), as it has one or more [child teams](#child-team) assigned to it.

## Permission

Defines a single permission enforced by an [application](#application). They are defined and declared by [applications](#application) using [security contracts](#security-contract). [Permissions](#permission) are then assigned to [functions](#function) by business users.

## Resource

A fundamental concept in any RESTful API is the resource. A resource is an object with a type, associated data, relationships to other resources, and a set of methods that operate on it. It is similar to an object instance in an object-oriented programming language, with the important difference that only a few standard methods are defined for the resource (corresponding to the standard HTTP GET, POST, PUT and DELETE methods), while an object instance typically has many methods.

## Role

A Role is grouping of [functions](#function). It is comprised of a unique name and a description that gives more information about the [role](#role). [Roles](#role) are assigned to [users](#user). **Note!** Roles can contain [functions](#function) from multiple [applications](#application).

## Scope

Scope is a mechanism in OAuth 2.0 to limit a client's access to [resources](#resource). A client can request one or more scopes on login and [access tokens](#access-token) issued to the client will be limited to the scopes granted.

OAuth does not define any particular values for scopes, since it is highly dependent on the service's internal architecture and needs.

## Security Contract

This is a declarative YAML document that allows idempotent definition of a desired [A3S](https://github.com/GrindrodBank/A3S) state. Almost all aspects of [A3S](https://github.com/GrindrodBank/A3S) can be defined using security contracts. Please refer to the [security contract documentation](./security-contracts.md) for a comprehensive guide to [security contracts](./security-contracts.md).

## Team

A team is a collection or group of [users](#user). 

[Data policies](#data-policy) are assigned to teams, which results in the members of that team inheriting the [data policies](#data-policy). Teams can also have other teams assigned to them, creating a [compound team](#parent-team). A [compound team](#parent-team) is also referred to as a [parent team](#parent-team), as it has one or more [child teams](#child-team) assigned to it. There are conditions for the creation of [compound teams](#parent-team), listed below:

* A [compound team](#parent-team) may not have [users](#user) directly assigned to it. It's [user](#user) membership consists of the members of all the [child teams](#child-team) assigned to it. Attempting to add [users](#user) to an existing [compound team](#parent-team), or create a [compound team](#parent-team) from a team that alreay has [users](#user) assigned, will result in error being returned by [A3S](https://github.com/GrindrodBank/A3S).
* A [team](#team) cannot be assigned to [team](#team) that is already a [child team](#child-team) of another [parent team](#parent-team), thus ensuring that [compound teams](#parent-team) only ever one depth of [child teams](#child-team).

## User

A user models a unique identity for an entity throughout the entire enterprise that is secured by [A3S](https://github.com/GrindrodBank/A3S).

## User Claim

A [user](#user) claim is a statement that a user makes about itself. For example a claim list can have the user’s name, user’s e-mail, user’s age, and user's authorization for an action. [A3S](https://github.com/GrindrodBank/A3S) issues [JWTs](#jwt) that contain `permission` and `dataPolicy` user claims, in addition to standard identification and more standard claims. A [JWT](#jwt) issued by [A3S](https://github.com/GrindrodBank/A3S) can be seen as a trusted and verified [user](#user) claims list.