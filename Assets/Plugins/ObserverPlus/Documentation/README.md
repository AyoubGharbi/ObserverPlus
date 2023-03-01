ObserverPlus is a Unity plugin that provides a base implementation for an observer pattern. 
The plugin consists of two base classes, AEvent<T> and AEventListener<T>, and an editor window, TemplateCreationFlow, for easily creating new event scripts.

# AEvent<T>

AEvent<T> is a base class for all scriptable object events that pass data of type T to its listeners. It has the following public methods and fields:

- GetListeners(): Returns a read-only list of all listeners that are registered to this event.
- RegisterListener(AEventListener<T> listener): Registers a new listener to this event and triggers the listener with the previous value of the event.
- UnregisterListener(AEventListener<T> listener): Unregisters a listener from this event.
- Raise(T eventValue): Raises the event by calling the OnEventRaised method of each registered listener and saving the current value as the previous value.

# AEventListener<T>
AEventListener<T> is a base class for creating event listeners that can listen to events of type AEvent<T>. 
It has the following public method and fields:

- OnEventRaised(T eventValue, T previousValue): Executes the UnityEvent action when the event is raised.

# TemplateCreationFlow
TemplateCreationFlow is an editor window for creating new ObserverPlus event scripts. 
It has the following public methods and fields:

- ShowWindow(): Displays the editor window for creating a new ObserverPlus event script.
- OnGUI(): Renders the GUI for the editor window.
- CreateScript(TemplateType templateType): Creates a new script of the given templateType. The available template types are Event, Listener, and EventEditor. 

The created scripts are placed in the "Plugins/ObserverPlus/Generated" folder of the project.

To use ObserverPlus, create a new event script by opening the editor window from the main menu "ObserverPlus/Create New Event Script".
Then, create a new script that extends AEvent<T> or AEventListener<T> as needed.
Finally, use the Raise(T eventValue) method of your AEvent<T> instance to trigger the event and pass its data to all listeners.

For more information, see the source code and examples provided in the plugin.