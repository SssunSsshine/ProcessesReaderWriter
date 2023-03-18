# ProcessesReaderWriter
5.	Варианты задачи: 1)механизмы синхронизации: события, семафоры, мьютексы, 2) структуры хранения данных (СХД): стек конечной длины или очередь FIFO конечной длины, 3) варианты условий
5.1.	Создать приложение с двумя дополнительными потоками писателей и двумя потоками читателей. Писатели в случайные моменты времени помещают записи, содержащие номер потока писателя и номер записи в СХД, читатели в случайные моменты времени удаляют записи, делая об этом сообщения. 
5.2.	Создать многопоточное приложение с одним потоком читателем, удаляющим данные из СХД. Главный поток в случайные моменты времени порождает потоки писатели, которые в случайные моменты времени помещают данные СХД, если в структуре имеется свободное место, или самоуничтожаются с соответствующим сообщением.

6.	Задача, полностью аналогичная 5, но потоки становятся процессами, механизм передачи данных: общая память. При реализации читателей и писателей предполагается, что возможно использование различных языков программирования (недопустимо размещать в общей памяти структуры данных, зависящих от языка программирования). Выдается с заменой варианта. 312
