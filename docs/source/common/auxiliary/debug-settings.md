DebugSettings
==================================================================

Класс с настройками режима отладки.

Свойства
------------------------------------------------------------------

**SaveResponse**
   Флаг необходимости безусловно сохранять ответ.

&emsp;**Type**: bool

**ClearDirectory**
   Флаг очистки директорию с отладочными данными.

&emsp;**Type**: bool

**LogFileName**
   Имя файла лога, сохраняемого в директории отладки.

&emsp;**Type**: string

**OutputDir**
   Имя директории для сохранения отладочных данных внутри директории приложения.

&emsp;**Type**: string

------------------------------------------------------------------
Методы
------------------------------------------------------------------

.. code-block:: csharp

   public DebugSettings(string outputDir, string logFile)

Конструктор.

.. code-block:: csharp

   public void Clear()

Очистка данных.

.. code-block:: csharp

   public T Deserialize<T>(string url, string json, JsonSerializerSettings settings)

Отладочная функция десериализации объектов.