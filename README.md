# Wieloplatformowy System Zarządzania Zadaniami (Centralny Planer)

System produktywności typu klient-serwer, pozwalający na zarządzanie zadaniami z poziomu przeglądarki internetowej, pulpitu komputera oraz telefonu komórkowego. Wszystkie aplikacje korzystają wspólnie z centralnej, plikowej bazy danych SQLite.

---

# Architektura Systemu i Wymagania

Projekt składa się z czterech powiązanych ze sobą modułów:
1. **Centralny Backend (`PlanerAPI`):** ASP.NET Core Web API (.NET 8) realizujący operacje CRUD na bazie danych.
2. **Aplikacja Webowa (`PlanerWeb`):** Responsywny interfejs użytkownika w Angularze.
3. **Aplikacja Desktopowa (`PlanerDesktop`):** Natywna aplikacja okienkowa Windows w WPF.
4. **Aplikacja Mobilna (`PlanerMobile`):** Aplikacja na smartfony (Android) w .NET MAUI.

---

# Instrukcja Uruchomienia

1. **Uruchomienie Backend API:** Uruchom projekt `PlanerAPI` w Visual Studio (`F5`). Serwer ruszy na `http://localhost:5216`.
2. **Uruchomienie Angulara:** W folderze projektu wpisz `npm install` oraz `ng serve -o`.
3. **Uruchomienie WPF / MAUI:** W Visual Studio kliknij prawym przyciskiem na dany projekt -> *Debuguj* -> *Uruchom nowe wystąpienie*.

---

# Dokument Testów Integracyjnych

| Id Testu | Opis scenariusza testowego | Oczekiwany rezultat | Wynik testu |
| :--- | :--- | :--- | :--- |
| **T-01** | Uruchomienie serwera API bez bazy danych. | EF Core automatycznie tworzy plik `planer.db`. | **ZALICZONY** |
| **T-02** | Dodanie zadania z aplikacji WPF. | Zadanie zostaje zapisane w bazie danych. | **ZALICZONY** |
| **T-03** | Odświeżenie aplikacji Angular. | Nowe zadanie z WPF pojawia się na stronie www. | **ZALICZONY** |
| **T-04** | Dodanie zadania z emulatora Androida. | Rekord trafia do bazy przez IP `10.0.2.2`. | **ZALICZONY** |
| **T-05** | Usunięcie zadania w aplikacji WPF. | Rekord znika ze wszystkich aplikacji na raz. | **ZALICZONY** |
