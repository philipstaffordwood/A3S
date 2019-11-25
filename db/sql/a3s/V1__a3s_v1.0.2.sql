--
-- *************************************************
-- Copyright (c) 2019, Grindrod Bank Limited
-- License MIT: https://opensource.org/licenses/MIT
-- **************************************************
--

-- These updates relate to A3S v1.0.2 updates

--
-- Name: terms_of_service; Type: TABLE; Schema: _a3s; Owner: postgres
--

CREATE TABLE _a3s.terms_of_service (
    id uuid NOT NULL,
    agreement_name text NOT NULL,
    version text NOT NULL,
    agreement_file bytea NOT NULL,
    changed_by uuid NOT NULL,
    sys_period tstzrange DEFAULT tstzrange(CURRENT_TIMESTAMP, NULL::timestamp with time zone) NOT NULL
);


ALTER TABLE _a3s.terms_of_service OWNER TO postgres;

--
-- Name: TABLE terms_of_service; Type: COMMENT; Schema: _a3s; Owner: postgres
--

COMMENT ON TABLE _a3s.terms_of_service IS 'Terms of service agreement entries that users agree to.';


--
-- Name: COLUMN terms_of_service.version; Type: COMMENT; Schema: _a3s; Owner: postgres
--

COMMENT ON COLUMN _a3s.terms_of_service.version IS 'The version of the agreement. Format is {year}.{number}, i.e. 2019.6.';


--
-- Name: COLUMN terms_of_service.agreement_file; Type: COMMENT; Schema: _a3s; Owner: postgres
--

COMMENT ON COLUMN _a3s.terms_of_service.agreement_file IS 'A .tar.gz file, containing two files with the terms agreement: 
- terms_of_service.html
- terms_of_service.css';

--
-- Name: terms_of_service terms_of_service_pk; Type: CONSTRAINT; Schema: _a3s; Owner: postgres
--

ALTER TABLE ONLY _a3s.terms_of_service
    ADD CONSTRAINT terms_of_service_pk PRIMARY KEY (id);

--
-- Name: terms_of_service uk_terms_of_service_agreement_name_version; Type: CONSTRAINT; Schema: _a3s; Owner: postgres
--

ALTER TABLE ONLY _a3s.terms_of_service
    ADD CONSTRAINT uk_terms_of_service_agreement_name_version UNIQUE (agreement_name, version);

--
-- Name: team; Type: TABLE; Schema: _a3s; Owner: postgres
--

ALTER TABLE _a3s.team
    ADD terms_of_service_id uuid NULL;

--
-- Name: team fk_team_terms_of_service_terms_of_service_id; Type: FK CONSTRAINT; Schema: _a3s; Owner: postgres
--

ALTER TABLE ONLY _a3s.team
    ADD CONSTRAINT fk_team_terms_of_service_terms_of_service_id FOREIGN KEY (terms_of_service_id) REFERENCES _a3s.terms_of_service(id) MATCH FULL ON DELETE RESTRICT;


