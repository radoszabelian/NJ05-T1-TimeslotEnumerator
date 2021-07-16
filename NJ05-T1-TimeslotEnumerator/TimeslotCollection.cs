using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJ05_T1_TimeslotEnumerator
{
    public class TimeslotEnumerator : IEnumerator
    {
        private int StartIndex;
        private int CurrentIndex;
        private (int Left, int Right) Diff;

        private bool isLeftNext = true;

        private string[] listRef;

        private bool isLeftEnded = false;
        private bool isRightEnded = false;

        public TimeslotEnumerator(string[] _listRef, int _StartIndex = 0)
        {
            listRef = _listRef;
            StartIndex = _StartIndex;
            Reset();
        }

        public bool MoveNext()
        {
            if (CurrentIndex == -1)
            { 
                CurrentIndex = StartIndex;
                return true;
            }

            isLeftEnded = StartIndex - (Diff.Left + 1) < 0;
            isRightEnded = StartIndex + (Diff.Right + 1) >= listRef.Length;

            if (isRightEnded || isLeftNext && !isLeftEnded)
            {
                Diff.Left++;
                CurrentIndex = StartIndex - Diff.Left;
                isLeftNext = false;
                return true;
            }
            else if (isLeftEnded || !isRightEnded)
            {
                Diff.Right++;
                CurrentIndex = StartIndex + Diff.Right;
                isLeftNext = true;
                return true;
            }

            return false;
        }

        public object Current => listRef[CurrentIndex];

        private bool IsInsideBounds(Direction boundsType, int index)
        {
            if (boundsType == Direction.Left) return index >= 0;
            return index < listRef.Length;
        }

        public void Reset()
        {
            CurrentIndex = -1;
            Diff.Left = 0;
            Diff.Right = 0;
        }
    }

    enum Direction
    {
        Left,
        Right
    };
}
