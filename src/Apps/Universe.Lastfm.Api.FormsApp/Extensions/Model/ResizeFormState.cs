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

using System.Drawing;
using System.Windows.Forms;

namespace Universe.Lastfm.Api.FormsApp.Extensions.Model
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class ResizeFormState
    {
        private Size _sourceSize;
        private Size _destinationSize;
        private Size _mainPanelSize;

        public ResizeFormState(Size sourceSize)
        {
            var diffWidth = sourceSize.Width - sourceSize.Width;
            var diffHeight = sourceSize.Height - sourceSize.Height;

            _sourceSize = new Size(sourceSize.Width, sourceSize.Height);
            _mainPanelSize = new Size(sourceSize.Width, sourceSize.Height);

            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            _destinationSize = new Size(resolution.Width - diffWidth, resolution.Height - diffHeight);

            IsMaximized = false;
        }

        public double WidthKoef => _destinationSize.Width / (_sourceSize.Width * 1.0);

        public double HeightKoef => _destinationSize.Height / (_sourceSize.Height * 1.0);

        public double WidthKoefReverse => _sourceSize.Width / (_destinationSize.Width * 1.0);

        public double HeightKoefReverse => _sourceSize.Height / (_destinationSize.Height * 1.0);

        public bool IsMaximized { get; set; }

        public void SetDestinationSize(Size sizeChanged)
        {
            _destinationSize = new Size(sizeChanged.Width, sizeChanged.Height);
        }

        public ResizeFormState ResizeUp(Control sender, Size destSize)
        {
            SetDestinationSize(destSize);
            this.ResizeUp(sender);

            _sourceSize = _destinationSize;
            return this;
        }
    }
}