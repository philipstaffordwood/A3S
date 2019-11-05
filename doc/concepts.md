# A3S Concepts

This portion of the documentation focuses on A3S specific concepts to give the reader more insight into why A3S was created, how the internals of A3S work and are inter-related, as well as some insight into how A3S was is intended to be used within an organization.

# Separation of Business and Technical Concerns

A3S has been created with an important assertion in mind. The users that administer access control throughout the organization are not technical people and don't want to view access control through a technical lense. [Resources](./glossary.md#resource), APIs, Scopes, etc are all technical terms about centralized access control. Modeling access control from a technical-first perspective results in a [resource](./glossary.md#resource)-centric view of the organization. This lands up with a model that effectively supports answering the following question: "Which conditions need to be satisfied for access to be granted to a resource?". How does the answer to that question relate to business functionality or capability? 

A3S models this domain in a more conventional, [user](./glossary.md#user)-centric fashion, with the objective of answering the following question. "Which [users](./glossary.md#user) have access to which business capabilities?". A3S also introduces the concept of fine-grained [user](./glossary.md#user) level [permissions](./glossary.md#permission)-based authorisation using Oauth 2.0 and OpenID Connect. Conventionally, these protocols control access to [clients](./glossary.md#client) (**not [users](./glossary.md#user)**) using RBAC (Roles Based Access Control) or [client](./glossary.md#client) [scope](./glossary.md#scope)-based access control.

## Make Technical Concerns a DevOps Item

Centralized Access control using OAuth2.0 and OpenID connect is a very technical endeavor. These technical concerns do need to be addressed and handled by technical people. Under the hood, [resources](./glossary.md#resource), APIs, [scopes](./glossary.md#scope), [clients](./glossary.md#client) etc still exists within A3S. Technical people are still going to write, update and deploy the [applications](./glossary.md#application) that supply [resources](./glossary.md#resource) on a continual basis. A3S has been designed to enable this process by shifting the technical aspects of this process to be more of a DevOps concern, rather than a run-time business administration concern. This is done using [security contracts](./glossary.md#security-contract).

## Security Contracts 

[Security Contracts](./glossary.md#security-contract) are a critical component of how this separation is achieved. [Applications](./glossary.md#application) and [clients](./glossary.md#client), which are the A3S components modelling [resource](./glossary.md#resource)-servers and OAuth 2.0 [clients](./glossary.md#client) respectively, can be declared by the technical teams responsible for them, within A3S [security contracts](./glossary.md#security-contract). These [security contracts](./glossary.md#security-contract) can then be idempotently applied (during a CI/CD deployment for example), to assert a declarative state of the technical aspects of that deployment.

## Abstracting Technical Concerns With Functions

[Applications](./glossary.md#application), and their [permissions](./glossary.md#permission) are lower-level, more technical representations of an [applications'](./glossary.md#application) [resources](./glossary.md#resource). [Functions](./glossary.md#function) allow the aggregation of these lower level items, into collections of more `Business` related functionality. These [functions](./glossary.md#function) can be created by technical teams within the [security contract](./glossary.md#security-contract) they supply on deployment, or by an A3S administrator using the A3S API or UI (coming soon). It is up to you to decide which actor (business or technical) is best suited to doing this mapping.

## Roles, Functions and Users.

A3S uses a [permissions](./glossary.md#permission)-based access control method. Conventionally, this means creating [roles](./glossary.md#role) (which are groupings of [permissions](./glossary.md#permission)). These [roles](./glossary.md#role) are then applied to [users](./glossary.md#user), thus giving the [users](./glossary.md#user) all the [permissions](./glossary.md#permission) within those roles. This also enables creating and changing [roles](./glossary.md#role) during run time, because [applications](./glossary.md#application) assert [permissions](./glossary.md#permission), and have no notion of any [roles](./glossary.md#role) or how those [permissions](./glossary.md#permission) are associated. A3S follows this paradigm. **However**, rather than assigning [permissions](./glossary.md#permission) to [roles](./glossary.md#role), A3S assigns the more business-abstracted [functions](./glossary.md#function) to [roles](./glossary.md#role).

[Roles](./glossary.md#role) are then assigned to [users](./glossary.md#user) accordingly.

