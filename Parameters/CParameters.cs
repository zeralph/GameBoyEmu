using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;
using GameBoyTest.Inputs;

namespace GameBoyTest.Parameters
{
    [Serializable] 
    public class CParameters
    {
        [Serializable] 
        public struct recentFile
        {
            public string path;
            public string name;
        }

        /// ///////////////////////////////////////////
        internal int _InterpolationMode=0;
        public int InterpolationMode
        {
            get { return _InterpolationMode; }
            set { _InterpolationMode = value; }
        }
        /// ///////////////////////////////////////////
        internal bool _Strech=true;
        public bool Strech
        {
            get { return _Strech; }
            set { _Strech = value; }
        }
        /// ///////////////////////////////////////////
        internal int _Speed = 100;
        public int Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }
        /// ///////////////////////////////////////////
        internal recentFile[] _Recent = new recentFile[5];
        public recentFile[] Recent
        {
            get { return _Recent; }
            set { _Recent = value; }
        }
        /// ///////////////////////////////////////////
        internal int _RecentFileIdx=-1;
        public int RecentFileIdx
        {
            get { return _RecentFileIdx; }
            set { _RecentFileIdx = value; }
        }
        /// ///////////////////////////////////////////
        internal int _NbRecentFile= 0;
        public int NbRecentFile
        {
            get { return _NbRecentFile; }
            set { _NbRecentFile = value; }
        }
        ///
        ///     INPUT SECTION
        /// 
        /// ///////////////////////////////////////////
        internal int _inputName = (int)InputsMgr.eInputName.e_inputName_keyboard;
        public int inputName
        {
            get { return _inputName; }
            set { _inputName = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputUp = (int)Keys.Up;
        public int inputUp
        {
            get { return _inputUp; }
            set { _inputUp = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputDown = (int)Keys.Down;
        public int inputDown
        {
            get { return _inputDown; }
            set { _inputDown = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputLeft = (int)Keys.Left;
        public int inputLeft
        {
            get { return _inputLeft; }
            set { _inputLeft = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputRight = (int)Keys.Right;
        public int inputRight
        {
            get { return _inputRight; }
            set { _inputRight = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputA = (int)Keys.A;
        public int inputA
        {
            get { return _inputA; }
            set { _inputA = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputB = (int)Keys.Z;
        public int inputB
        {
            get { return _inputB; }
            set { _inputB = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputStart = (int)Keys.Enter;
        public int inputStart
        {
            get { return _inputStart; }
            set { _inputStart = value; }
        }
        /// ///////////////////////////////////////////
        internal int _inputSelect = (int)Keys.Space;
        public int inputSelect
        {
            get { return _inputSelect; }
            set { _inputSelect = value; }
        }
    }
}
