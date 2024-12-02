Select * from [Empresa].[Sucursal]


UPDATE [Empresa].[Sucursal]
SET Latitud = CASE WHEN Latitud = '' THEN NULL ELSE Latitud END
WHERE Latitud = ''


UPDATE [Empresa].[Sucursal]
SET Longitud = CASE WHEN Longitud = '' THEN NULL ELSE Longitud END
WHERE Longitud = ''