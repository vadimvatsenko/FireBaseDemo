using System.Drawing;

namespace Auth
{
    public interface IPageView // интерфейс, показывать или скрывать страницу
    {        
        public void Show(); // показать
        public void Hide(); // скрыть
    }
}