using System;

namespace ProductLibrary_lab
{
    // Делегат и класс аргументов выносим в общую библиотеку, 
    // чтобы они были доступны и коллекции, и тем, кто на неё подписывается.

    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class CollectionHandlerEventArgs : EventArgs
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public object ChangedObject { get; set; }

        public CollectionHandlerEventArgs(string name, string changeType, object changedObj)
        {
            CollectionName = name;
            ChangeType = changeType;
            ChangedObject = changedObj;
        }

        public override string ToString()
        {
            return $"[{CollectionName}] {ChangeType}: {ChangedObject}";
        }
    }
}