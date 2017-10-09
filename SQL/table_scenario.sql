create table if not exists bigdata.api_key (
	api text,
    client_id text,
    client_secret text
);


create table if not exists bigdata.scenario_bigdata(
	id_module varchar(20),
    id_user int,
    id_scenario int,
    scen_name text,
    empresa text,
    grupo_analise text,
    periodicidade text,
	flag_marga int,
    scen_type char(1),
    language text,
    location text,
    flag_location int,
    type_period char(10),
    map_selection char(10)
);

create table if not exists bigdata.scenario_ocorrencia (
    id_scenario int,
	word text,
    qtd int,
    origem text,
    itempesquisa text,
    status text,
    idpost text
);

create table if not exists bigdata.scenario_dados (
	id_scenario int,
    item text,
    positivo text,
    neutro text,
    negativo text,
    periodo text,
    idpost text
);

create table if not exists bigdata.scenario_localizacao (
	id_scenario int,
    itempesquisa text,
    status text,
    latitude text,
    longitude text,
    origem text,
    address text,
    idpost text
)