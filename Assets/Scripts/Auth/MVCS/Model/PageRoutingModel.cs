
namespace Auth
{
    public class PageRoutingModel // 
    {
        public Observable<CurrentPage> CurrentPageValue { get; private set; } = new(CurrentPage.Login);  // тут будет хранится текущая страница,
                                                                                                          // в данном случае это Login,                                                                                                          // в других классах можно будет перезаписывать  значение                                                                                                        // другое значение
    }
}