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
using Universe.Lastfm.Api.Dto;
using Universe.Lastfm.Api.Infrastracture;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Dal.Queries
{
    public abstract class BaseQuery : CQRS.Dal.Queries.Base.BaseQuery
    {
        protected virtual Func<BaseRequest, BaseResponce> ExecutableBaseFunc => null;

        public ApiUser ApiUser { get; internal set; }

        public RootDto Root { get; internal set; }

        internal virtual void Init(IUniverseLastApiSettings settings) { }

        public virtual BaseResponce ExecuteBase(BaseRequest request)
        {
            if (request != null)
                if (ExecutableBaseFunc != null)
                    return ExecutableBaseFunc.Invoke(request);

            return new BaseResponce() {
                Message = "The logic wasn't implemented. Specify 'ExecutableBaseFunc' in your implementation."
            };
        }

        public virtual BaseResponce ExecuteBaseSafe(
            BaseRequest request)
        {
            try
            {
                return ExecuteBase(request);
            }
            catch (Exception ex)
            {
                return new BaseResponce()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }

    public abstract class BaseQuery<TRequest, TResponce> : BaseQuery
        where TRequest : BaseRequest
        where TResponce : BaseResponce, new()
    {
        public virtual TResponce Execute(TRequest request)
        {
            if (request != null)
                if (ExecutableBaseFunc != null)
                    return ExecutableBaseFunc.Invoke(request) as TResponce;

            return new TResponce() {
                Message = "The logic wasn't implemented!"
            };
        }

        public virtual TResponce ExecuteSafe(
            TRequest request)
        {
            try
            {
                return Execute(request);
            }
            catch (Exception ex)
            {
                return new TResponce()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}