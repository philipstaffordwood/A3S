--
-- *************************************************
-- Copyright (c) 2019, Grindrod Bank Limited
-- License MIT: https://opensource.org/licenses/MIT
-- **************************************************
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 10.9
-- Dumped by pg_dump version 11.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: ApiClaims; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ApiClaims" (
    "Id" integer NOT NULL,
    "Type" character varying(200) NOT NULL,
    "ApiResourceId" integer NOT NULL
);


ALTER TABLE _ids4."ApiClaims" OWNER TO postgres;

--
-- Name: ApiClaims_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ApiClaims_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ApiClaims_Id_seq" OWNER TO postgres;

--
-- Name: ApiClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ApiClaims_Id_seq" OWNED BY _ids4."ApiClaims"."Id";


--
-- Name: ApiProperties; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ApiProperties" (
    "Id" integer NOT NULL,
    "Key" character varying(250) NOT NULL,
    "Value" character varying(2000) NOT NULL,
    "ApiResourceId" integer NOT NULL
);


ALTER TABLE _ids4."ApiProperties" OWNER TO postgres;

--
-- Name: ApiProperties_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ApiProperties_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ApiProperties_Id_seq" OWNER TO postgres;

--
-- Name: ApiProperties_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ApiProperties_Id_seq" OWNED BY _ids4."ApiProperties"."Id";


--
-- Name: ApiResources; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ApiResources" (
    "Id" integer NOT NULL,
    "Enabled" boolean NOT NULL,
    "Name" character varying(200) NOT NULL,
    "DisplayName" character varying(200),
    "Description" character varying(1000),
    "Created" timestamp without time zone NOT NULL,
    "Updated" timestamp without time zone,
    "LastAccessed" timestamp without time zone,
    "NonEditable" boolean NOT NULL
);


ALTER TABLE _ids4."ApiResources" OWNER TO postgres;

--
-- Name: ApiResources_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ApiResources_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ApiResources_Id_seq" OWNER TO postgres;

--
-- Name: ApiResources_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ApiResources_Id_seq" OWNED BY _ids4."ApiResources"."Id";


--
-- Name: ApiScopeClaims; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ApiScopeClaims" (
    "Id" integer NOT NULL,
    "Type" character varying(200) NOT NULL,
    "ApiScopeId" integer NOT NULL
);


ALTER TABLE _ids4."ApiScopeClaims" OWNER TO postgres;

--
-- Name: ApiScopeClaims_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ApiScopeClaims_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ApiScopeClaims_Id_seq" OWNER TO postgres;

--
-- Name: ApiScopeClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ApiScopeClaims_Id_seq" OWNED BY _ids4."ApiScopeClaims"."Id";


--
-- Name: ApiScopes; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ApiScopes" (
    "Id" integer NOT NULL,
    "Name" character varying(200) NOT NULL,
    "DisplayName" character varying(200),
    "Description" character varying(1000),
    "Required" boolean NOT NULL,
    "Emphasize" boolean NOT NULL,
    "ShowInDiscoveryDocument" boolean NOT NULL,
    "ApiResourceId" integer NOT NULL
);


ALTER TABLE _ids4."ApiScopes" OWNER TO postgres;

--
-- Name: ApiScopes_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ApiScopes_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ApiScopes_Id_seq" OWNER TO postgres;

--
-- Name: ApiScopes_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ApiScopes_Id_seq" OWNED BY _ids4."ApiScopes"."Id";


--
-- Name: ApiSecrets; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ApiSecrets" (
    "Id" integer NOT NULL,
    "Description" character varying(1000),
    "Value" character varying(4000) NOT NULL,
    "Expiration" timestamp without time zone,
    "Type" character varying(250) NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "ApiResourceId" integer NOT NULL
);


ALTER TABLE _ids4."ApiSecrets" OWNER TO postgres;

--
-- Name: ApiSecrets_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ApiSecrets_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ApiSecrets_Id_seq" OWNER TO postgres;

--
-- Name: ApiSecrets_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ApiSecrets_Id_seq" OWNED BY _ids4."ApiSecrets"."Id";


--
-- Name: ClientClaims; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientClaims" (
    "Id" integer NOT NULL,
    "Type" character varying(250) NOT NULL,
    "Value" character varying(250) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientClaims" OWNER TO postgres;

--
-- Name: ClientClaims_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientClaims_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientClaims_Id_seq" OWNER TO postgres;

--
-- Name: ClientClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientClaims_Id_seq" OWNED BY _ids4."ClientClaims"."Id";


--
-- Name: ClientCorsOrigins; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientCorsOrigins" (
    "Id" integer NOT NULL,
    "Origin" character varying(150) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientCorsOrigins" OWNER TO postgres;

--
-- Name: ClientCorsOrigins_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientCorsOrigins_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientCorsOrigins_Id_seq" OWNER TO postgres;

--
-- Name: ClientCorsOrigins_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientCorsOrigins_Id_seq" OWNED BY _ids4."ClientCorsOrigins"."Id";


--
-- Name: ClientGrantTypes; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientGrantTypes" (
    "Id" integer NOT NULL,
    "GrantType" character varying(250) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientGrantTypes" OWNER TO postgres;

--
-- Name: ClientGrantTypes_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientGrantTypes_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientGrantTypes_Id_seq" OWNER TO postgres;

--
-- Name: ClientGrantTypes_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientGrantTypes_Id_seq" OWNED BY _ids4."ClientGrantTypes"."Id";


--
-- Name: ClientIdPRestrictions; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientIdPRestrictions" (
    "Id" integer NOT NULL,
    "Provider" character varying(200) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientIdPRestrictions" OWNER TO postgres;

--
-- Name: ClientIdPRestrictions_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientIdPRestrictions_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientIdPRestrictions_Id_seq" OWNER TO postgres;

--
-- Name: ClientIdPRestrictions_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientIdPRestrictions_Id_seq" OWNED BY _ids4."ClientIdPRestrictions"."Id";


--
-- Name: ClientPostLogoutRedirectUris; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientPostLogoutRedirectUris" (
    "Id" integer NOT NULL,
    "PostLogoutRedirectUri" character varying(2000) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientPostLogoutRedirectUris" OWNER TO postgres;

--
-- Name: ClientPostLogoutRedirectUris_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientPostLogoutRedirectUris_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientPostLogoutRedirectUris_Id_seq" OWNER TO postgres;

--
-- Name: ClientPostLogoutRedirectUris_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientPostLogoutRedirectUris_Id_seq" OWNED BY _ids4."ClientPostLogoutRedirectUris"."Id";


--
-- Name: ClientProperties; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientProperties" (
    "Id" integer NOT NULL,
    "Key" character varying(250) NOT NULL,
    "Value" character varying(2000) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientProperties" OWNER TO postgres;

--
-- Name: ClientProperties_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientProperties_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientProperties_Id_seq" OWNER TO postgres;

--
-- Name: ClientProperties_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientProperties_Id_seq" OWNED BY _ids4."ClientProperties"."Id";


--
-- Name: ClientRedirectUris; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientRedirectUris" (
    "Id" integer NOT NULL,
    "RedirectUri" character varying(2000) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientRedirectUris" OWNER TO postgres;

--
-- Name: ClientRedirectUris_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientRedirectUris_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientRedirectUris_Id_seq" OWNER TO postgres;

--
-- Name: ClientRedirectUris_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientRedirectUris_Id_seq" OWNED BY _ids4."ClientRedirectUris"."Id";


--
-- Name: ClientScopes; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientScopes" (
    "Id" integer NOT NULL,
    "Scope" character varying(200) NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientScopes" OWNER TO postgres;

--
-- Name: ClientScopes_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientScopes_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientScopes_Id_seq" OWNER TO postgres;

--
-- Name: ClientScopes_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientScopes_Id_seq" OWNED BY _ids4."ClientScopes"."Id";


--
-- Name: ClientSecrets; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."ClientSecrets" (
    "Id" integer NOT NULL,
    "Description" character varying(2000),
    "Value" character varying(4000) NOT NULL,
    "Expiration" timestamp without time zone,
    "Type" character varying(250) NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "ClientId" integer NOT NULL
);


ALTER TABLE _ids4."ClientSecrets" OWNER TO postgres;

--
-- Name: ClientSecrets_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."ClientSecrets_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."ClientSecrets_Id_seq" OWNER TO postgres;

--
-- Name: ClientSecrets_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."ClientSecrets_Id_seq" OWNED BY _ids4."ClientSecrets"."Id";


--
-- Name: Clients; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."Clients" (
    "Id" integer NOT NULL,
    "Enabled" boolean NOT NULL,
    "ClientId" character varying(200) NOT NULL,
    "ProtocolType" character varying(200) NOT NULL,
    "RequireClientSecret" boolean NOT NULL,
    "ClientName" character varying(200),
    "Description" character varying(1000),
    "ClientUri" character varying(2000),
    "LogoUri" character varying(2000),
    "RequireConsent" boolean NOT NULL,
    "AllowRememberConsent" boolean NOT NULL,
    "AlwaysIncludeUserClaimsInIdToken" boolean NOT NULL,
    "RequirePkce" boolean NOT NULL,
    "AllowPlainTextPkce" boolean NOT NULL,
    "AllowAccessTokensViaBrowser" boolean NOT NULL,
    "FrontChannelLogoutUri" character varying(2000),
    "FrontChannelLogoutSessionRequired" boolean NOT NULL,
    "BackChannelLogoutUri" character varying(2000),
    "BackChannelLogoutSessionRequired" boolean NOT NULL,
    "AllowOfflineAccess" boolean NOT NULL,
    "IdentityTokenLifetime" integer NOT NULL,
    "AccessTokenLifetime" integer NOT NULL,
    "AuthorizationCodeLifetime" integer NOT NULL,
    "ConsentLifetime" integer,
    "AbsoluteRefreshTokenLifetime" integer NOT NULL,
    "SlidingRefreshTokenLifetime" integer NOT NULL,
    "RefreshTokenUsage" integer NOT NULL,
    "UpdateAccessTokenClaimsOnRefresh" boolean NOT NULL,
    "RefreshTokenExpiration" integer NOT NULL,
    "AccessTokenType" integer NOT NULL,
    "EnableLocalLogin" boolean NOT NULL,
    "IncludeJwtId" boolean NOT NULL,
    "AlwaysSendClientClaims" boolean NOT NULL,
    "ClientClaimsPrefix" character varying(200),
    "PairWiseSubjectSalt" character varying(200),
    "Created" timestamp without time zone NOT NULL,
    "Updated" timestamp without time zone,
    "LastAccessed" timestamp without time zone,
    "UserSsoLifetime" integer,
    "UserCodeType" character varying(100),
    "DeviceCodeLifetime" integer NOT NULL,
    "NonEditable" boolean NOT NULL
);


ALTER TABLE _ids4."Clients" OWNER TO postgres;

--
-- Name: Clients_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."Clients_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."Clients_Id_seq" OWNER TO postgres;

--
-- Name: Clients_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."Clients_Id_seq" OWNED BY _ids4."Clients"."Id";


--
-- Name: IdentityClaims; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."IdentityClaims" (
    "Id" integer NOT NULL,
    "Type" character varying(200) NOT NULL,
    "IdentityResourceId" integer NOT NULL
);


ALTER TABLE _ids4."IdentityClaims" OWNER TO postgres;

--
-- Name: IdentityClaims_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."IdentityClaims_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."IdentityClaims_Id_seq" OWNER TO postgres;

--
-- Name: IdentityClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."IdentityClaims_Id_seq" OWNED BY _ids4."IdentityClaims"."Id";


--
-- Name: IdentityProperties; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."IdentityProperties" (
    "Id" integer NOT NULL,
    "Key" character varying(250) NOT NULL,
    "Value" character varying(2000) NOT NULL,
    "IdentityResourceId" integer NOT NULL
);


ALTER TABLE _ids4."IdentityProperties" OWNER TO postgres;

--
-- Name: IdentityProperties_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."IdentityProperties_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."IdentityProperties_Id_seq" OWNER TO postgres;

--
-- Name: IdentityProperties_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."IdentityProperties_Id_seq" OWNED BY _ids4."IdentityProperties"."Id";


--
-- Name: IdentityResources; Type: TABLE; Schema: _ids4; Owner: postgres
--

CREATE TABLE _ids4."IdentityResources" (
    "Id" integer NOT NULL,
    "Enabled" boolean NOT NULL,
    "Name" character varying(200) NOT NULL,
    "DisplayName" character varying(200),
    "Description" character varying(1000),
    "Required" boolean NOT NULL,
    "Emphasize" boolean NOT NULL,
    "ShowInDiscoveryDocument" boolean NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "Updated" timestamp without time zone,
    "NonEditable" boolean NOT NULL
);


ALTER TABLE _ids4."IdentityResources" OWNER TO postgres;

--
-- Name: IdentityResources_Id_seq; Type: SEQUENCE; Schema: _ids4; Owner: postgres
--

CREATE SEQUENCE _ids4."IdentityResources_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE _ids4."IdentityResources_Id_seq" OWNER TO postgres;

--
-- Name: IdentityResources_Id_seq; Type: SEQUENCE OWNED BY; Schema: _ids4; Owner: postgres
--

ALTER SEQUENCE _ids4."IdentityResources_Id_seq" OWNED BY _ids4."IdentityResources"."Id";


--
-- Name: ApiClaims Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiClaims" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ApiClaims_Id_seq"'::regclass);


--
-- Name: ApiProperties Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiProperties" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ApiProperties_Id_seq"'::regclass);


--
-- Name: ApiResources Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiResources" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ApiResources_Id_seq"'::regclass);


--
-- Name: ApiScopeClaims Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiScopeClaims" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ApiScopeClaims_Id_seq"'::regclass);


--
-- Name: ApiScopes Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiScopes" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ApiScopes_Id_seq"'::regclass);


--
-- Name: ApiSecrets Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiSecrets" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ApiSecrets_Id_seq"'::regclass);


--
-- Name: ClientClaims Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientClaims" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientClaims_Id_seq"'::regclass);


--
-- Name: ClientCorsOrigins Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientCorsOrigins" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientCorsOrigins_Id_seq"'::regclass);


--
-- Name: ClientGrantTypes Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientGrantTypes" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientGrantTypes_Id_seq"'::regclass);


--
-- Name: ClientIdPRestrictions Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientIdPRestrictions" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientIdPRestrictions_Id_seq"'::regclass);


--
-- Name: ClientPostLogoutRedirectUris Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientPostLogoutRedirectUris" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientPostLogoutRedirectUris_Id_seq"'::regclass);


--
-- Name: ClientProperties Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientProperties" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientProperties_Id_seq"'::regclass);


--
-- Name: ClientRedirectUris Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientRedirectUris" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientRedirectUris_Id_seq"'::regclass);


--
-- Name: ClientScopes Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientScopes" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientScopes_Id_seq"'::regclass);


--
-- Name: ClientSecrets Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientSecrets" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."ClientSecrets_Id_seq"'::regclass);


--
-- Name: Clients Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."Clients" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."Clients_Id_seq"'::regclass);


--
-- Name: IdentityClaims Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityClaims" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."IdentityClaims_Id_seq"'::regclass);


--
-- Name: IdentityProperties Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityProperties" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."IdentityProperties_Id_seq"'::regclass);


--
-- Name: IdentityResources Id; Type: DEFAULT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityResources" ALTER COLUMN "Id" SET DEFAULT nextval('_ids4."IdentityResources_Id_seq"'::regclass);


--
-- Name: ApiClaims_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ApiClaims_Id_seq"', 1, true);


--
-- Name: ApiProperties_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ApiProperties_Id_seq"', 1, false);


--
-- Name: ApiResources_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ApiResources_Id_seq"', 1, true);


--
-- Name: ApiScopeClaims_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ApiScopeClaims_Id_seq"', 1, false);


--
-- Name: ApiScopes_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ApiScopes_Id_seq"', 1, true);


--
-- Name: ApiSecrets_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ApiSecrets_Id_seq"', 1, false);


--
-- Name: ClientClaims_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientClaims_Id_seq"', 1, false);


--
-- Name: ClientCorsOrigins_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientCorsOrigins_Id_seq"', 1, false);


--
-- Name: ClientGrantTypes_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientGrantTypes_Id_seq"', 1, true);


--
-- Name: ClientIdPRestrictions_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientIdPRestrictions_Id_seq"', 1, false);


--
-- Name: ClientPostLogoutRedirectUris_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientPostLogoutRedirectUris_Id_seq"', 1, false);


--
-- Name: ClientProperties_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientProperties_Id_seq"', 1, false);


--
-- Name: ClientRedirectUris_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientRedirectUris_Id_seq"', 1, false);


--
-- Name: ClientScopes_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientScopes_Id_seq"', 3, true);


--
-- Name: ClientSecrets_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."ClientSecrets_Id_seq"', 1, true);


--
-- Name: Clients_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."Clients_Id_seq"', 1, true);


--
-- Name: IdentityClaims_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."IdentityClaims_Id_seq"', 15, true);


--
-- Name: IdentityProperties_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."IdentityProperties_Id_seq"', 1, false);


--
-- Name: IdentityResources_Id_seq; Type: SEQUENCE SET; Schema: _ids4; Owner: postgres
--

SELECT pg_catalog.setval('_ids4."IdentityResources_Id_seq"', 2, true);


--
-- Name: ApiClaims PK_ApiClaims; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiClaims"
    ADD CONSTRAINT "PK_ApiClaims" PRIMARY KEY ("Id");


--
-- Name: ApiProperties PK_ApiProperties; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiProperties"
    ADD CONSTRAINT "PK_ApiProperties" PRIMARY KEY ("Id");


--
-- Name: ApiResources PK_ApiResources; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiResources"
    ADD CONSTRAINT "PK_ApiResources" PRIMARY KEY ("Id");


--
-- Name: ApiScopeClaims PK_ApiScopeClaims; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiScopeClaims"
    ADD CONSTRAINT "PK_ApiScopeClaims" PRIMARY KEY ("Id");


--
-- Name: ApiScopes PK_ApiScopes; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiScopes"
    ADD CONSTRAINT "PK_ApiScopes" PRIMARY KEY ("Id");


--
-- Name: ApiSecrets PK_ApiSecrets; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiSecrets"
    ADD CONSTRAINT "PK_ApiSecrets" PRIMARY KEY ("Id");


--
-- Name: ClientClaims PK_ClientClaims; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientClaims"
    ADD CONSTRAINT "PK_ClientClaims" PRIMARY KEY ("Id");


--
-- Name: ClientCorsOrigins PK_ClientCorsOrigins; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientCorsOrigins"
    ADD CONSTRAINT "PK_ClientCorsOrigins" PRIMARY KEY ("Id");


--
-- Name: ClientGrantTypes PK_ClientGrantTypes; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientGrantTypes"
    ADD CONSTRAINT "PK_ClientGrantTypes" PRIMARY KEY ("Id");


--
-- Name: ClientIdPRestrictions PK_ClientIdPRestrictions; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientIdPRestrictions"
    ADD CONSTRAINT "PK_ClientIdPRestrictions" PRIMARY KEY ("Id");


--
-- Name: ClientPostLogoutRedirectUris PK_ClientPostLogoutRedirectUris; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientPostLogoutRedirectUris"
    ADD CONSTRAINT "PK_ClientPostLogoutRedirectUris" PRIMARY KEY ("Id");


--
-- Name: ClientProperties PK_ClientProperties; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientProperties"
    ADD CONSTRAINT "PK_ClientProperties" PRIMARY KEY ("Id");


--
-- Name: ClientRedirectUris PK_ClientRedirectUris; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientRedirectUris"
    ADD CONSTRAINT "PK_ClientRedirectUris" PRIMARY KEY ("Id");


--
-- Name: ClientScopes PK_ClientScopes; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientScopes"
    ADD CONSTRAINT "PK_ClientScopes" PRIMARY KEY ("Id");


--
-- Name: ClientSecrets PK_ClientSecrets; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientSecrets"
    ADD CONSTRAINT "PK_ClientSecrets" PRIMARY KEY ("Id");


--
-- Name: Clients PK_Clients; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."Clients"
    ADD CONSTRAINT "PK_Clients" PRIMARY KEY ("Id");


--
-- Name: IdentityClaims PK_IdentityClaims; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityClaims"
    ADD CONSTRAINT "PK_IdentityClaims" PRIMARY KEY ("Id");


--
-- Name: IdentityProperties PK_IdentityProperties; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityProperties"
    ADD CONSTRAINT "PK_IdentityProperties" PRIMARY KEY ("Id");


--
-- Name: IdentityResources PK_IdentityResources; Type: CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityResources"
    ADD CONSTRAINT "PK_IdentityResources" PRIMARY KEY ("Id");


--
-- Name: IX_ApiClaims_ApiResourceId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ApiClaims_ApiResourceId" ON _ids4."ApiClaims" USING btree ("ApiResourceId");


--
-- Name: IX_ApiProperties_ApiResourceId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ApiProperties_ApiResourceId" ON _ids4."ApiProperties" USING btree ("ApiResourceId");


--
-- Name: IX_ApiResources_Name; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE UNIQUE INDEX "IX_ApiResources_Name" ON _ids4."ApiResources" USING btree ("Name");


--
-- Name: IX_ApiScopeClaims_ApiScopeId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ApiScopeClaims_ApiScopeId" ON _ids4."ApiScopeClaims" USING btree ("ApiScopeId");


--
-- Name: IX_ApiScopes_ApiResourceId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ApiScopes_ApiResourceId" ON _ids4."ApiScopes" USING btree ("ApiResourceId");


--
-- Name: IX_ApiScopes_Name; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE UNIQUE INDEX "IX_ApiScopes_Name" ON _ids4."ApiScopes" USING btree ("Name");


--
-- Name: IX_ApiSecrets_ApiResourceId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ApiSecrets_ApiResourceId" ON _ids4."ApiSecrets" USING btree ("ApiResourceId");


--
-- Name: IX_ClientClaims_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientClaims_ClientId" ON _ids4."ClientClaims" USING btree ("ClientId");


--
-- Name: IX_ClientCorsOrigins_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientCorsOrigins_ClientId" ON _ids4."ClientCorsOrigins" USING btree ("ClientId");


--
-- Name: IX_ClientGrantTypes_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientGrantTypes_ClientId" ON _ids4."ClientGrantTypes" USING btree ("ClientId");


--
-- Name: IX_ClientIdPRestrictions_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientIdPRestrictions_ClientId" ON _ids4."ClientIdPRestrictions" USING btree ("ClientId");


--
-- Name: IX_ClientPostLogoutRedirectUris_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientPostLogoutRedirectUris_ClientId" ON _ids4."ClientPostLogoutRedirectUris" USING btree ("ClientId");


--
-- Name: IX_ClientProperties_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientProperties_ClientId" ON _ids4."ClientProperties" USING btree ("ClientId");


--
-- Name: IX_ClientRedirectUris_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientRedirectUris_ClientId" ON _ids4."ClientRedirectUris" USING btree ("ClientId");


--
-- Name: IX_ClientScopes_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientScopes_ClientId" ON _ids4."ClientScopes" USING btree ("ClientId");


--
-- Name: IX_ClientSecrets_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_ClientSecrets_ClientId" ON _ids4."ClientSecrets" USING btree ("ClientId");


--
-- Name: IX_Clients_ClientId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Clients_ClientId" ON _ids4."Clients" USING btree ("ClientId");


--
-- Name: IX_IdentityClaims_IdentityResourceId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_IdentityClaims_IdentityResourceId" ON _ids4."IdentityClaims" USING btree ("IdentityResourceId");


--
-- Name: IX_IdentityProperties_IdentityResourceId; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE INDEX "IX_IdentityProperties_IdentityResourceId" ON _ids4."IdentityProperties" USING btree ("IdentityResourceId");


--
-- Name: IX_IdentityResources_Name; Type: INDEX; Schema: _ids4; Owner: postgres
--

CREATE UNIQUE INDEX "IX_IdentityResources_Name" ON _ids4."IdentityResources" USING btree ("Name");


--
-- Name: ApiClaims FK_ApiClaims_ApiResources_ApiResourceId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiClaims"
    ADD CONSTRAINT "FK_ApiClaims_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES _ids4."ApiResources"("Id") ON DELETE CASCADE;


--
-- Name: ApiProperties FK_ApiProperties_ApiResources_ApiResourceId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiProperties"
    ADD CONSTRAINT "FK_ApiProperties_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES _ids4."ApiResources"("Id") ON DELETE CASCADE;


--
-- Name: ApiScopeClaims FK_ApiScopeClaims_ApiScopes_ApiScopeId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiScopeClaims"
    ADD CONSTRAINT "FK_ApiScopeClaims_ApiScopes_ApiScopeId" FOREIGN KEY ("ApiScopeId") REFERENCES _ids4."ApiScopes"("Id") ON DELETE CASCADE;


--
-- Name: ApiScopes FK_ApiScopes_ApiResources_ApiResourceId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiScopes"
    ADD CONSTRAINT "FK_ApiScopes_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES _ids4."ApiResources"("Id") ON DELETE CASCADE;


--
-- Name: ApiSecrets FK_ApiSecrets_ApiResources_ApiResourceId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ApiSecrets"
    ADD CONSTRAINT "FK_ApiSecrets_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES _ids4."ApiResources"("Id") ON DELETE CASCADE;


--
-- Name: ClientClaims FK_ClientClaims_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientClaims"
    ADD CONSTRAINT "FK_ClientClaims_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientCorsOrigins FK_ClientCorsOrigins_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientCorsOrigins"
    ADD CONSTRAINT "FK_ClientCorsOrigins_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientGrantTypes FK_ClientGrantTypes_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientGrantTypes"
    ADD CONSTRAINT "FK_ClientGrantTypes_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientIdPRestrictions FK_ClientIdPRestrictions_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientIdPRestrictions"
    ADD CONSTRAINT "FK_ClientIdPRestrictions_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientPostLogoutRedirectUris FK_ClientPostLogoutRedirectUris_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientPostLogoutRedirectUris"
    ADD CONSTRAINT "FK_ClientPostLogoutRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientProperties FK_ClientProperties_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientProperties"
    ADD CONSTRAINT "FK_ClientProperties_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientRedirectUris FK_ClientRedirectUris_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientRedirectUris"
    ADD CONSTRAINT "FK_ClientRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientScopes FK_ClientScopes_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientScopes"
    ADD CONSTRAINT "FK_ClientScopes_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: ClientSecrets FK_ClientSecrets_Clients_ClientId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."ClientSecrets"
    ADD CONSTRAINT "FK_ClientSecrets_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES _ids4."Clients"("Id") ON DELETE CASCADE;


--
-- Name: IdentityClaims FK_IdentityClaims_IdentityResources_IdentityResourceId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityClaims"
    ADD CONSTRAINT "FK_IdentityClaims_IdentityResources_IdentityResourceId" FOREIGN KEY ("IdentityResourceId") REFERENCES _ids4."IdentityResources"("Id") ON DELETE CASCADE;


--
-- Name: IdentityProperties FK_IdentityProperties_IdentityResources_IdentityResourceId; Type: FK CONSTRAINT; Schema: _ids4; Owner: postgres
--

ALTER TABLE ONLY _ids4."IdentityProperties"
    ADD CONSTRAINT "FK_IdentityProperties_IdentityResources_IdentityResourceId" FOREIGN KEY ("IdentityResourceId") REFERENCES _ids4."IdentityResources"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

