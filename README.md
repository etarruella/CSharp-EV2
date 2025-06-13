# Inventario

Vamos a expandir la funcionalidad de los items haciendo que estos se guardar en distintos sitios y hasta equiparse.

> [!warning]
> Implementad solo lo que se pide, con los nombres que se pide y sin añadir lógica innecesaria. Al entregar entregad solo los archivos que contienen las clases y funcionalidades especificadas en un archivo **.zip**

## Parte 1: PlayerInventory (4 puntos)

En primer lugar, empezaremos con el inventario de los jugadores, para el cual os he dejado una plantilla. La idea es que proporcione todas las funcionalidades de gestión de items que necesita el jugador de forma normal.

### 1.1 Lógica básica (2 Puntos)

Lo primero es que haga las **funcionalidades elementales** que esperamos de un sistema de inventario:

1. Que limite el número de items (no puedes superar la capacidad)
2. Poder almacenar items (con `Store`)
   - Retorna false si no puede guardarlo
3. Poder ver el item almacenado en una posición con `GetItemAt`
   - Null si no hay o se sale del rango
4. Poder descartar items (con `Drop`)
   - Retorna false si no puede soltarlo
5. Visualizar los items que tiene (con `ListItems`)

Cómo decidís **almacenar los items es cosa vuestra**, usad el sistema que considereis **más adecuado** y **justificad** vuestra decisión si lo considerais necesario. El único requisito, de momento (luego añadiremos más), es que todos los elementos almacenados y no descartados se muestren al realizar el `ListItems` y que los datos estén debidamente encapsulados.

### 1.2 Lógica Mejorada (1 Punto)

Sin embargo, para el funcionamiento adecuado de este sistema necesitamos añadir más restricciones a su comportamiento y nuevas funcionalidades. Por ejemplo:

1. Necesitamos que el mismo item (la misma instancia) no pueda cogerse **por duplicado**
   - (Que el mismo item aparezca como si fueran dos distintos es una fuente garantizada de bugs)
   - `Store` debe devolver `true` si el item ya está siendo almacenado pero sin almacenarlo.
2. Queremos que se **preserve el orden de inserción**.
   - Si insertamos tres items (con `Store`) uno detrás de otro, `ListItems` los debería mostrar en ese mismo orden.
3. El orden se ha de preservar **incluso cuando descartamos** items.
   - Si inserto 3 elementos y luego suelto el 1º, los otros dos deben seguir siendo el 2º y el 3º, **no avanzar una posición**.
   - Otro ejemplo páctico: Soltar los 3 primeros elementos uno detrás de otro ha de ser `Drop(0);Drop(1);Drop(2);`, no `Drop(0);Drop(0);Drop(0);`
   - (Esto es en parte para no desorientar al jugador haciendo que se le muevan los items)

### 1.3 Más Funcionalidades (1 Punto)

Técnicamente ya podemos hacer todas las cosas que queremos con este inventario, pero aún no es del todo práctico de usar. Las siguientes funciones nos permiten simplificar el uso de la clase y evitarnos errores tontos.

1. `Clear`: Borra el contenido del inventario.
2. `StoreAt`: Similar a `Store` pero guarda el item en una posición concreta.
   - A diferencia de `Store`, `StoreAt` si que devuelve `false` si intentas insertar un item que ya está en el inventario a no ser que el item esté en la misma posición exacta en la que querías insertarlo.
3. `Contains`: Devuelve true si el item especificado está almacenado en el inventario.
4. `Find`: Encuentra el primer Item que cumpla la condición especificada en el delegado.
5. `Find<T>`: Lo mismo pero también filtra por subtipo de item que busca.
6. `Transfer`: Parecido a Drop o Store pero transfiere un item desde el inventario actual a otro inventario objetivo. Retorna false si algo falla.


> [!warning]
> RECORDATORIO: si no lograis hacer algo no dejeis el código con errores. Haced lo que podais, **comentad el código** que no funciona y **explicad que os falla** pero NUNCA DEJEIS EL CÓDIGO CON ERRORES DE COMPILACIÓN.
>
> Para evitar disgustos recordad comprobar todas las cosas antes de entregarlas, no supongais que los cambios que habeis hecho no van a causar problemas.


## Parte 2: Más Inventarios (2 Puntos)

Obviamente no todo van a ser jugadores. Partiendo de `PlayerInventory`, implementa las siguientes clases con las siguientes características:

1. `ChestInventory` para gestionar los items que se encuentran en el escenario.
2. `NPCInventory` Para gestionar los items que llevan los personajes no jugables (`NPC`)
3. `ShopInventory` para gestionar los items que se venden en una tienda.

Estas clases son similares pero tienen las siguientes diferencias:

1. `ShopInventory` no tiene límite de items, pero solo acepta items con precio de venta (no null).
2. Solo se puede extraer items de `ChestInventory` una vez ha sido creado, no insertarlos.
3. `NPCInventory` no puede hacer `Drop` de los items, solo cogerlos o transferirlos.

Recordad implementar esto de forma que sea compatible con el sistema previo y que permita transferir entre distintos tipos de inventario.