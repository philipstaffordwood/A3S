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
    sys_period tstzrange DEFAULT tstzrange(CURRENT_TIMESTAMP, NULL::timestamp with time zone)
);

ALTER TABLE _a3s.terms_of_service OWNER TO postgres;

COMMENT ON TABLE _a3s.terms_of_service IS 'Terms of service agreement entries that users agree to.';

ALTER TABLE ONLY _a3s.terms_of_service
    ADD CONSTRAINT pk_terms_of_service PRIMARY KEY (id);


--
-- Name: team; Type: TABLE; Schema: _a3s; Owner: postgres
--

ALTER TABLE _a3s.team
    ADD terms_of_service_id uuid NULL;

ALTER TABLE ONLY _a3s.team
    ADD CONSTRAINT team_terms_of_service_id_fkey FOREIGN KEY (terms_of_service_id) REFERENCES _a3s.terms_of_service(id) ON DELETE RESTRICT;
