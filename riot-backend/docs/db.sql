create schema if not exists extensions;

-- make sure everybody can use everything in the extensions schema
grant usage on schema extensions to public;
grant execute on all functions in schema extensions to public;

-- include future extensions
alter default privileges in schema extensions
    grant execute on functions to public;

alter default privileges in schema extensions
    grant usage on types to public;

CREATE EXTENSION if not exists citext schema extensions;
CREATE TYPE login_method AS ENUM ('google');


create schema if not exists public;

SET search_path TO public, extensions;
/**SET search_path TO test, extensions;**/
CREATE TABLE if not exists users
(
    id         SERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name  VARCHAR(100) NOT NULL,
    email      citext       NOT NULL,
    type       login_method NOT NULL,
    token      text
);
CREATE UNIQUE INDEX ON users ((lower(email)));

CREATE TABLE if not exists teams
(
    id         SERIAL PRIMARY KEY,
    name       VARCHAR(100) NOT NULL,
    tft_set    smallint     NOT NULL,
    created_by integer REFERENCES users (id),
    create_on  timestamp
);

CREATE TABLE if not exists team_champions
(
    championId varchar(50) PRIMARY KEY,
    team_id    integer REFERENCES teams (id),
    item_ids   smallint[]
);