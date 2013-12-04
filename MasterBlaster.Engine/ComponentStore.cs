using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBlaster.Engine
{
   public class ComponentStore
    {
       private List<IComponent> _components;

       public ComponentStore(List<IComponent> components)
       {
           _components = components;
       }

       public ComponentStore()
       {
           _components = new List<IComponent>();
       }

       public void Add<T>(T component) where T : IComponent
       {
           _components.Add(component);
       }

       public void Remove<T>(T component) where T : IComponent
       {
           _components.Remove(component);
       }

       public void RemoveAll<T>() where T : IComponent
       {
           _components.RemoveAll(c => c is T);
       }

       public T GetSingle<T>() where T : IComponent
       {
           return (T)_components.Single(c => c is T);
       }
       public List<T> GetAllOfType<T>() where T : IComponent
       {
           var selectedComponents = _components.Where(c => c is T).ToList();

           return new List<T>(selectedComponents.Cast<T>());
       }
    }
}
