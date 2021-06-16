<h2>Rough implementation of vector lines</h2>

<h4>Included is two prefabs making the system work.</h4>
-Connection Point
-Wire

<h4>Instructions</h4>
Simply place a connection point wherever you want to be able to connect another
connection point to. You can link sets of connection points by assigning the "Circuit Connection"
variable to the targeted.

Visit the Example scene for a mini sandbox/demo

- You can change the color of the wire by editing or changing the prefab the connection point references.

- You can set the color of the connection point invidually and the color it changes to when it is being interacted with.

- You can delete a connection by re-dragging the connection point (This will likely change to something simpler/more logical)


<h4>TODO:</h4>
- Create a better connection removal system (Something simpler thats more intuitive)
- Create a visual connection between linked connection points (For example, a thin line
that shows all of a row in a breadboard are connected.)
- Maybe make the connection points target represent a doubly linked list rather than a singly
to make tracing connections easier.
- Create midwire connection points that can be moved so organizing cables is easier
- Implement a connection tool that hides and shows connection points when its being used. (This may not be necessary depending on how we implement this system)
- Feedback from team
- Debugging
