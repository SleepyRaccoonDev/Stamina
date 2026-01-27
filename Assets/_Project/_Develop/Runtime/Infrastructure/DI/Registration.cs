using System;

namespace Assets.Project._Develop.Runtime.Infrastructure.DI
{
    public class Registration
    {
        private Func<DIConteiner, object> _creator;
        private object _cachedInstance;

        public Registration(Func<DIConteiner, object> creator) => _creator = creator;

        public object CreateInstamceFrom(DIConteiner conteiner)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("Creator or Instance is not exist!");

            _cachedInstance = _creator.Invoke(conteiner);

            return _cachedInstance;
        }
    }
}