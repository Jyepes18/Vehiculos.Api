-- nombre de la DB vehiculos_inspecciones

create table vehiculos (
   id SERIAL PRIMARY KEY,
   placa VARCHAR(10) NOT NULL,
   marca VARCHAR(100) NOT NULL,
   modelo VARCHAR(100) NOT NULL,
   anio INTEGER NOT NULL,
   fecha_registro TIMESTAMP NOT NULL,
   activo BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS inspecciones (
    id SERIAL PRIMARY KEY,
    vehiculo_id INTEGER NOT NULL,
    fecha TIMESTAMP NOT NULL,
    kilometraje INTEGER NOT NULL,
    resultado VARCHAR(20) NOT NULL,
    observaciones VARCHAR(1000),

    CONSTRAINT fk_inspecciones_vehiculo
    FOREIGN KEY (vehiculo_id)
    REFERENCES vehiculos(id)
);
