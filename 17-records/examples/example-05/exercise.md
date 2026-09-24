# Ejercicio 5 — Comparar copias sin usar records

## Contexto

Este ejercicio no usa records en ningún momento — es, a propósito, el camino largo. Sirve para comprobar que entiendes qué es exactamente lo que un record te ahorra escribir.

## Requisitos

1. Declara un `struct` (no un record struct) llamado `Ejemplar`, con `CodigoBarras` (`string`) y `Disponible` (`bool`).

2. Crea dos instancias de `Ejemplar` con los mismos valores exactos y compáralas con `==`. Antes de corregir nada, observa qué ocurre al intentar compilar.

3. Haz que la comparación del punto 2 funcione, considerando dos ejemplares iguales cuando (y solo cuando) coincidan tanto en `CodigoBarras` como en `Disponible`. No existe ningún atajo automático aquí — tendrás que escribir tú mismo la lógica de la comparación, en la forma que este tema ha mostrado para este tipo de casos.

4. El propio lenguaje exige que, si defines esa comparación, definas también su contraria. Hazlo, reutilizando lo que ya escribiste en el punto 3 en vez de repetir la lógica desde cero.

## Para comprobar que funciona

Con los cambios de los puntos 3 y 4, la comparación del punto 2 debe compilar y devolver `true` para dos ejemplares con los mismos valores, y `false` en cuanto cualquiera de los dos datos difiera entre ellos.