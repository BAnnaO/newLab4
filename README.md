Лабораторная работа 4. Варианты первого уровня
Делегаты. События
Информация для всех вариантов 
В лабораторной работе требуется определить класс, содержащий типизированную коллекцию, который с помощью событий извещает об изменениях в коллекции.
Коллекция состоит из объектов ссылочных типов. Коллекция изменяется при удалении/добавлении элементов или при изменении одной из входящих в коллекцию ссылок, например, когда одной из ссылок присваивается новое значение. В этом случае в соответствующих методах или свойствах класса бросаются события.
При изменении данных объектов, ссылки на которые входят в коллекцию, значения самих ссылок не изменяются. Этот тип изменений не порождает событий.
Для событий, извещающих об изменениях в коллекции, определяется свой делегат. События регистрируются в специальных классах-слушателях. 
Вариант 3. Требования к программе
Определить новую версию класса ResearchTeamCollection из лабораторной работы 3, которая  с помощью событий сообщает об изменениях в коллекции.
Для событий  определить делегат TeamListHandler с сигнатурой:

void TeamListHandler 
     (object source, TeamListHandlerEventArgs args);
Класс TeamListHandlerEventArgs, производный от класса System.EventArgs, содержит
•	открытое автореализуемое свойство типа string с названием коллекции, в которой произошло событие;
•	открытое автореализуемое свойство типа string с информацией о типе изменений в коллекции;
•	открытое автореализуемое свойство типа int с номером элемента, который был добавлен или заменен;
•	конструкторы для инициализации класса;
•	перегруженную версию метода string ToString() для формирования строки с информацией обо всех полях класса.
В новую версию класса ResearchTeamCollection добавить 
•	открытое автореализуемое свойство типа string с названием коллекции;
•	метод void InsertAt (int j, ResearchTeam rt), который вставляет элемент rt в список List<ResearchTeam> перед элементом с номером j; если в списке нет элемента с номером j, метод добавляет элемент в конец списка;
•	индексатор типа ResearchTeam  (с методами get и set) с целочисленным индексом для доступа к элементу списка List<ResearchTeam> с заданным номером.
В новую версию класса ResearchTeamCollection добавить два события типа TeamListHandler
•	ResearchTeamAdded, которое происходит при добавлении элемента в конец списка List<ResearchTeam>; cобытие  передает через объект TeamListHandlerEventArgs  имя коллекции, строку с информацией о том, что в коллекцию был добавлен элемент, и номер добавленного элемента в списке List<ResearchTeam>; 
•	ResearchTeamInserted, которое происходит, когда новый элемент вставляется перед одним из элементов списка List<ResearchTeam>; событие передает через объект TeamListHandlerEventArgs  имя коллекции, строку с информацией о том, что в коллекцию был вставлен элемент, и номер нового элемента.
Событие ResearchTeamAdded бросают методы класса ResearchTeamCollection
•	AddDefaults();
•	AddResearchTeams ( params  ResearchTeam []); 
•	InsertAt (int j, ResearchTeam rt), если элемента с номером j нет в списке.
Событие ResearchTeamInserted бросает метод InsertAt (int j, ResearchTeam rt), если элемент с номером j есть в списке List<ResearchTeam>.
Определить класс TeamsJournal для накопления информации об изменениях в коллекциях типа ResearchTeamCollection. В классе TeamsJournal информация хранится в списке из элементов типа TeamsJournalEntry, каждый элемент списка содержит информацию об отдельном изменении в коллекции ResearchTeamCollection. 
Класс TeamsJournalEntry содержит 
•	открытое автореализуемое свойство типа string с названием коллекции, в которой произошло событие;
•	открытое автореализуемое свойство типа string с информацией о том, какое событие произошло в коллекции;
•	номер нового элемента;
•	конструктор для инициализации полей класса;
•	перегруженную версию метода string ToString().
Класс TeamsJournal содержит 
•	закрытое поле List<TeamsJournalEntry> для  списка изменений;
•	обработчик событий ResearchTeamAdded и ResearchTeamInserted.  Обработчик использует информацию, которая передается ему через объект TeamListHandlerEventArgs, создает элемент TeamsJournalEntry и добавляет его в список List<TeamsJournalEntry>.
•	перегруженную версию метода string ToString() для формирования строки с информацией обо всех элементах списка List<TeamsJournalEntry>.
В методе Main()
1.	Создать две коллекции ResearchTeamCollection.
2.	Создать два объекта типа TeamsJournal, один объект TeamsJournal подписать на события ResearchTeamAdded и ResearchTeamInserted из первой коллекции ResearchTeamCollection, другой объект TeamsJournal подписать на события ResearchTeamInserted из обеих коллекций ResearchTeamCollection.
3.	Внести изменения в коллекции ResearchTeamCollection
•	добавить элементы в коллекции;
•	с помощью  метода InsertAt (int j, ResearchTeam rt) перед элементом с номером j, который есть в коллекции, вставить новый элемент;
•	вызвать метод InsertAt (int j, ResearchTeam rt) с номером j, которого нет в коллекции.
4.	Вывести данные обоих объектов TeamsJournal.