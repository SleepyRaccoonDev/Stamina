using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Infrastructure.DI
{
    public class DIConteiner
    {
        public readonly Dictionary<Type, Registration> _container = new();

        private readonly List<Type> _requests =new();

        private readonly DIConteiner _parent;

        public DIConteiner(DIConteiner parent) => _parent = parent;

        public DIConteiner() : this(null)
        {

        }


        public void RegisterAsSingle<T>(Func<DIConteiner, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} is already register");

            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsAlreadyRegister<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstamceFrom(this);

                if (_parent != null)
                    _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} is not founded!");
        }
    }
}