using System.Collections.Generic;

namespace Pinokio.Designer
{
    /// <summary>

    /// 개수 제한이 있는 스택 클래스

    /// </summary>

    internal class LimitedStack<T>

    {

        List<T> list = new List<T>();

        readonly int capacity;

        /// <summary>

        /// 개수를 가져온다.

        /// </summary>

        public int Count

        {

            get { return this.list.Count; }

        }



        /// <summary>

        /// 생성자

        /// </summary>

        /// <param name="capacity"></param>

        public LimitedStack(int capacity)

        {

            this.capacity = capacity;

        }



        /// <summary>

        /// 맨위의 개체를 반환하고 제거한다.

        /// </summary>

        /// <returns></returns>

        internal T Pop()

        {

            T t = this.list[0];

            this.list.RemoveAt(0);

            return t;

        }



        /// <summary>

        /// 개체를 맨위에 삽입한다.

        /// </summary>

        /// <param name="state"></param>

        internal void Push(T state)

        {

            this.list.Insert(0, state);

            if (this.list.Count > capacity)

            {

                this.list.RemoveAt(this.list.Count - 1);

            }

        }



        /// <summary>

        /// 맨위의 개체를 제거하지 않고 반환한다.

        /// </summary>

        /// <returns></returns>

        internal T Peek()

        {

            return this.list[0];

        }



        /// <summary>

        /// 개체를 모두 제거한다.

        /// </summary>

        internal void Clear()

        {

            this.list.Clear();

        }
    }

}

