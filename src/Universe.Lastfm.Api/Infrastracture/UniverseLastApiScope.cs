//  ╔═════════════════════════════════════════════════════════════════════════════════╗
//  ║                                                                                 ║
//  ║   Copyright 2024 Universe.Lastfm.Api                                            ║
//  ║                                                                                 ║
//  ║   Licensed under the Apache License, Version 2.0 (the "License");               ║
//  ║   you may not use this file except in compliance with the License.              ║
//  ║   You may obtain a copy of the License at                                       ║
//  ║                                                                                 ║
//  ║       http://www.apache.org/licenses/LICENSE-2.0                                ║
//  ║                                                                                 ║
//  ║   Unless required by applicable law or agreed to in writing, software           ║
//  ║   distributed under the License is distributed on an "AS IS" BASIS,             ║
//  ║   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.      ║
//  ║   See the License for the specific language governing permissions and           ║
//  ║   limitations under the License.                                                ║
//  ║                                                                                 ║
//  ║                                                                                 ║
//  ║   Copyright 2024 Universe.Lastfm.Api                                            ║
//  ║                                                                                 ║
//  ║   Лицензировано согласно Лицензии Apache, Версия 2.0 ("Лицензия");              ║
//  ║   вы можете использовать этот файл только в соответствии с Лицензией.           ║
//  ║   Вы можете найти копию Лицензии по адресу                                      ║
//  ║                                                                                 ║
//  ║       http://www.apache.org/licenses/LICENSE-2.0.                               ║
//  ║                                                                                 ║
//  ║   За исключением случаев, когда это регламентировано существующим               ║
//  ║   законодательством или если это не оговорено в письменном соглашении,          ║
//  ║   программное обеспечение распространяемое на условиях данной Лицензии,         ║
//  ║   предоставляется "КАК ЕСТЬ" и любые явные или неявные ГАРАНТИИ ОТВЕРГАЮТСЯ.    ║
//  ║   Информацию об основных правах и ограничениях,                                 ║
//  ║   применяемых к определенному языку согласно Лицензии,                          ║
//  ║   вы можете найти в данной Лицензии.                                            ║
//  ║                                                                                 ║
//  ╚═════════════════════════════════════════════════════════════════════════════════╝

using System;
using System.Data.Entity;
using Unity;
using Universe.CQRS.Infrastructure;
using Universe.CQRS.Models.Dto;
using Universe.CQRS.Models.Enums;

namespace Universe.Lastfm.Api.Infrastracture
{
    /// <summary>
    ///     Cфера деятельности/Рамки/Возможности сайта,
    ///     контекстом пользователя
    /// </summary>
    public class UniverseLastApiScope : IUniverseScope
    {
        private UserDto _user;

        private IUniverseLastApiSettings _appSettings;

        /// <summary>
        ///     Инициализирует экземпляр класса <see cref="UniverseLastApiScope"/>
        /// </summary>
        /// <param name="appSettings">Настройки веб-приложения</param>
        /// <param name="container">Unity-контейнер</param>
        public UniverseLastApiScope(IUniverseLastApiSettings appSettings, IUnityContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException(nameof(container));

            _appSettings = appSettings;
            Container = container;
        }

        public DbSystemManagementTypes DbSystemManagementType { get; set; }

        public IUnityContainer Container { get; }

        public DbContext DbCtx { get; }

        public UnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     Текущий пользователь приложения
        /// </summary>
        public UserDto CurrentUser
        {
            get
            {
                return this._user ??= this.GetUser();
            }
        }

        public Guid SessionId { get; }

        /// <summary>
        ///     Текущий выбранная локаль
        /// </summary>
        public string CurrentLocale { get; set; }

        /// <summary>
        ///     Сопоставление пользователя с личностью
        /// </summary>
        /// <returns>Возвращает пользователя приложения</returns>
        protected UserDto GetUser()
        {
            UserDto userDto = new UserDto();

            return userDto;
        }

        /// <summary>
        ///     Делает копию <see cref="UniverseLastApiScope"/>
        /// </summary>
        /// <returns>
        ///     Cфера деятельности/Рамки/Возможности сайта Universe Website с базой данных,
        ///     контекстом пользователя
        /// </returns>
        public UniverseLastApiScope Clone()
        {
            return new UniverseLastApiScope(_appSettings, Container);
        }
    }
}