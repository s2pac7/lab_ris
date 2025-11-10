# project7-buses

Учебный проект на базе **Contoso School**: веб-приложение на ASP.NET Web Forms + EF (Database First) и консольное приложение (будет добавлено отдельным участником).

## Состав решения
- **WebApp** — ASP.NET Web Forms + Entity Framework (DB First) на учебной БД `School.mdf`.  
  Реализовано: `Students.aspx`, `StudentsAdd.aspx`, `Courses.aspx`, `Enrollments.aspx`.
- **ConsoleApp** — будет добавлен отдельным PR (см. ниже «Как добавить Console»).

---

## Запуск Web (быстрый старт)
**Требования:** Visual Studio 2022, .NET Framework 4.8, LocalDB `(MSSQLLocalDB)`.

1. Откройте `Project7.sln` в Visual Studio.  
2. **Build → Rebuild Solution**.  
3. Запустите проект **WebApp** (F5).  
4. Навигация из меню: **Students**, **Add Student**, **Courses**, **Enrollments**.

---

## Что реализовано (Web)

### Технологии и модель
- **EF Database First**: `SchoolModel.edmx`, контекст `SchoolEntities`.
- Диаграмма связей: `Students —< Enrollments >— Courses`, `Departments —< Courses`.
- База данных: `App_Data/School.mdf`.

### Страницы
- **Students.aspx**
  - `GridView`: paging, sorting, edit, delete.
  - Формат: имя — `LastName, FirstMidName`, дата — `{0:yyyy-MM-dd}`.
  - Поиск по имени/фамилии.
  - Обработка ошибок при удалении связанных записей — дружелюбное сообщение (без падений).
  - Серверная проверка даты при редактировании (строгий `yyyy-MM-dd`, не в будущем).
- **StudentsAdd.aspx**
  - `DetailsView` в режиме **Insert**.
  - Серверная валидация обязательных полей; дата `yyyy-MM-dd`, не в будущем.
  - После успешной вставки — редирект на **Students** (новый студент виден сразу).
- **Courses.aspx**
  - `DropDownList` кафедр → фильтрует список курсов в `GridView`.
- **Enrollments.aspx**
  - Зачисление студента на курс.
  - Защита от дубликатов («уже записан на этот курс»).
  - Список зачислений студента с удалением.

### Поведение БД/ошибки
- Удаление студента: допускается **ON DELETE CASCADE** на `Enrollments(StudentID)` либо перехват исключения с понятным сообщением (в проекте обработка реализована).
- Источники данных — `SqlDataSource` (эквивалент `EntityDataSource`); сортировка работает через `DataSourceMode="DataSet"`.
- Все ошибки валидируются/перехватываются и показываются пользователю; приложение не «падает».

---

## Соответствие заданию (Web)
- ✅ Список/поиск/сортировка/редактирование/удаление студентов.  
- ✅ Вставка нового студента видна в общем списке сразу.  
- ✅ Выбор кафедры влияет на вывод курсов.  
- ✅ Нет ошибок при удалении связанных записей (каскад или обработка).  

---
<img width="997" height="352" alt="image" src="https://github.com/user-attachments/assets/4571234d-ae12-4db4-a45f-4dc51837c16a" />
<img width="986" height="298" alt="image" src="https://github.com/user-attachments/assets/d5075144-efe8-4c31-a5b0-6873c728a8f1" />
<img width="1343" height="335" alt="image" src="https://github.com/user-attachments/assets/c951ef13-e9e9-4d3b-9c9e-adbcd0431c34" />
<img width="1357" height="484" alt="image" src="https://github.com/user-attachments/assets/d78ab5de-3682-4990-a10e-8109a6bc50a2" />
<img width="708" height="261" alt="image" src="https://github.com/user-attachments/assets/e87a3bb2-2f91-4b03-9c61-d7855d04f2ef" />

## Запуск ConsoleApp (быстрый старт)
**Требования:** Visual Studio 2022 / 2023, .NET 8.0.

1. Убедитесь, что в папке `ConsoleApp/Data/` есть файл `buses.xml`.  
2. Запустите проект **ConsoleApp** (F5).  
3. Введите ID рейса для получения отчета о пассажирах.  
4. Логи ошибок сохраняются в `ConsoleApp/Logs/app.log`.

---

## Что реализовано (ConsoleApp)

### Технологии и модель
- **ООП**:  
  - Базовый класс `Bus` с 3+ методами, 1–2 `virtual`.  
  - Наследник `Passenger` как `partial` класс (поля/свойства и методы/константы/статические поля).  
- **Метод расширения** (`PassengerExtensions`) для коллекций пассажиров.  
- **Делегат/событие** для регистрации изменений пассажиров (`PassengerRegistered`, `PassengerCanceled`).  
- **LINQ to XML**: чтение, фильтры, проекции, агрегаты.  

### Данные и операции
- **Входные данные:** `Data/buses.xml`  
- **Операции:**  
  - Выбор рейса по ID  
  - Регистрация и отмена пассажиров  
  - Формирование отчетов:
    - `Reports/report_trip.txt` — сведения по выбранному рейсу (всего/занято/свободно + список пассажиров)  
    - `Reports/report_all_trips.txt` — свод по всем рейсам  
- **Обработка ошибок:** try/catch с логированием в `Logs/app.log`.  
- Пограничные случаи: рейс без пассажиров, неизвестный ID рейса.

---

## Пример работы

**Входной файл `buses.xml`:**

```xml
<Buses>
  <Bus ID="1" Route="Минск — Гродно" Seats="50">
    <Passenger ID="101" Name="Иванов Иван" />
    <Passenger ID="102" Name="Петров Пётр" />
  </Bus>
  <Bus ID="2" Route="Минск — Брест" Seats="40" />
</Buses>


