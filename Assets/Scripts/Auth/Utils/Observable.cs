namespace Auth
{
    using System;

    public class Observable<T> // дженерик класс который будет принимать дженериковое значение // в данном случае будет принимать страницу
    {
        private T _value; // дженерик значение
        
        public event Action<T, T> OnValueChanged; // дженериковое событие, принимает два дженериковых значения

        public Observable(T initialValue) // конструктор, принимает дженериковое значение
        {
            _value = initialValue; 
        }

        public Observable() // пустой конструктор
        {
        }

        public T Value // свойство, отдает значение
        {
            get => _value;
            set
            {
                if (!Equals(_value, value)) // если предыдущее значение и текущее не равны
                {
                    T oldValue = _value; // записываем старое значение страницы
                    _value = value; // перезаписываем значение
                    
                    OnValueChanged?.Invoke(oldValue, _value); // событие которое будет вызываться.
                    // обычно сюда приходят две страницы, обработка происходит в PageRoutingController, смена страниц
                }
            }
        }
    }
}
