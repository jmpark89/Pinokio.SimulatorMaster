using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer.DataClass
{
    public class UndoRedoHistory<T>
    {

        const int DefaultUndoCount = 30;

        LimitedStack<T> undoStack;
        LimitedStack<T> redoStack;
        /// <summary>
        /// Undo 기능을 사용할 수 있는지 여부를 가져온다.
        /// </summary>

        public bool IsCanUndo
        {
            get
            {

               //맨 초기 상태때문에 1보다 커야한다.
                return this.undoStack.Count > 0;

            }

        }



        /// <summary>

        /// Redo 기능을 사용할 수 있는지 여부를 가져온다.

        /// </summary>

        public bool IsCanRedo

        {

            get { return this.redoStack.Count > 0; }

        }



        public UndoRedoHistory()
            : this(DefaultUndoCount)
        {

        }



        public UndoRedoHistory(int defaultUndoCount)
        {
            undoStack = new LimitedStack<T>(defaultUndoCount);
            redoStack = new LimitedStack<T>(defaultUndoCount);
        }



        /// <summary>

        /// 이전 상태를 가져온다.

        /// </summary>

        /// <returns></returns>

        public T Undo()

        {

            T state = this.undoStack.Pop();

            this.redoStack.Push(state);

            return state;

        }



        /// <summary>

        /// 이후 상태를 가져온다.

        /// </summary>

        /// <returns></returns>

        public T Redo()

        {

            T state = this.redoStack.Pop();

            this.undoStack.Push(state);

            return state;

        }



        /// <summary>

        /// 상태를 추가한다.

        /// </summary>

        /// <param name="state"></param>

        public void AddState(T state)

        {

            this.undoStack.Push(state);

            this.redoStack.Clear();

        }



        /// <summary>

        /// 상태를 모두 제거한다.

        /// </summary>

        internal void Clear()

        {

            this.undoStack.Clear();

            this.redoStack.Clear();

        }

    }
}
