# MaquinitaJuegos
Template para creacion de juegos

Instrucciones: 

1) Crear un nuevo proyecto de Unity (ejemplo "Nuevo Proyecto").

2) Abrir "MaquinitaJuegos" en Unity, exportar como paquete la totalidad del contenido de la carpeta Assets, a excepción de:
    a) Archivos .json (si los hubiera).
    b) Escenas "Game 2" y "Game 3" (en el caso de que el juego sólo necesite un único nivel/modo de juego).

3) Importar el paquete recién creado dentro de la carpeta Assets de Nuevo Proyecto.

4) Todo el contenido de las escenas "Game" se encuentran dentro de Game Prefab. Por ende, sugerimos que cualquier cambio se haga dentro del prefab, a excepción de:  
    a) Nuevos GameObjects particulares de cada escena, los cuales no se repitirían en las demás escenas (quedarían por fuera del prefab).
    b) Componentes y propiedades de GameObjects, al interior del prefab, que no se repitan en las demás escenas.

5) (agregar explicación bloqueo de niveles no completados)

6) Para agregar más escenas de Game (más niveles y/o modos de juego):
    a) Crear nueva escena.
    b) Agregar Game Prefab dentro de la nueva escena.
    c) (Sin abrir prefab) Desde el inspector, editar la variable "Menu Scene Index" de GameManager.cs con el índice de escena correspondiente.
    d) (En escena Menu) Agregar nuevo botón de nivel como hijo de Levels Buttons (el cual posee Vertical Layout Group como componente).

7) Para enviar Json de Lista de Partidas al backend (cuando el sistema de envíos ya esté disponible):
    a) Descomentar variables "ID_..." de struct Attempt en SaveSystem.cs
    b) Desarrollar lógica para recibir estos datos desde el backend
    c) Asignar valores correspondientes en dichas variables en el método AddNewAttempt de GameManager.cs
    d) Utilizar métodos AddHit() y AddError() de TestAttempt.cs para agregar hits y errores cuando corresponda
    e) Llamar a método DeleteListAttempts() de SaveSystem.cs para borrar json luego de su envío al backend
