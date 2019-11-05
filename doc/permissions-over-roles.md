# Permissions Over Roles

## Introduction

Classic AAA systems limits access-control to roles. This is known as Role-based Access Control (RBAC).

With RBAC, a shared role is assigned to a group of users. This role usually defines the user's job title, and through this, permissions are inferred to limit what the user has access to.

Usually this causes administrators to see the permission model as level based, where the top level is the super-admin level with access to everything, and the lower level with the least amount of priviledge.

Unfortunately, this very rigid structure does not fit very well in most organizations, as different users in the same role or job title very often have different access requirements. Additionally, if the business were to change these roles, the underlying applications implementing the restrictions would also need to change.

## The A3S Approach

This is where A3S is different. A3S's approach is to give a comprehensive outlook to role and permission access. It gives applications the power to control what users can access on a finely grained level.

To do this, A3S implements a [permissions](./glossary.md#permission) based access control system. This means that your [Application](./glossary.md#application) will enforce that the accessing [user](./glossary.md#user) has the required [permissions](./glossary.md#permission) to access the [resources](./glossary.md#resource) the [user](./glossary.md#user) requested. 

At the moment of checking for [permissions](./glossary.md#permission), the [application](./glossary.md#application) only needs to know "does [user](./glossary.md#user) X have [permission](./glossary.md#permission) to access [resource](./glossary.md#resource) Y?". The [application](./glossary.md#application) does not care about, and should not be aware of relationships between [roles](./glossary.md#role), [functions](./glossary.md#function) and [permissions](./glossary.md#permission). 

This is very beneficial, as it, unlike a traditional RBAC system, frees up business users to create or alter [functions](./glossary.md#function), [roles](./glossary.md#role) and the [users](./glossary.md#user) they are assigned to within [A3S](https://github.com/GrindrodBank/A3S) without requiring developers to make any changes to the [application](./glossary.md#application). 

## Conclusion

[Roles](./glossary.md#role) and [functions]((./glossary.md#function)) are a business level concerns, and model business capabilities and groups of people within an organisation. Coupling a business level concern to a technical one (adding code and re-deploying an [appplication](./glossary.md#application)) is an impediment to the organisation. 

Permissions based access control allows for the business and technical levels of access control to be completely decoupled and enable much more flexibility aggregating [permissions](./glossary.md#permission) into [functions](./glossary.md#function) assigned to [roles](./glossary.md#role) and [users](./glossary.md#user).