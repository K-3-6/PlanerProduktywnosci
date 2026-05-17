Ten plik wyjaśnia, jak Visual Studio utworzyło projekt.

Następujące narzędzia zostały użyte do wygenerowania tego projektu:
- Angular CLI (ng)

Następujące kroki zostały użyte do wygenerowania tego projektu:
- Tworzenie projektu Angular za pomocą narzędzia NG: `ng new planer-web --defaults --skip-install --skip-git --no-standalone `.
- Zaktualizuj angular.json przy użyciu portu.
- Utwórz plik projektu (`planer-web.esproj`).
- Utwórz `launch.json`, aby włączyć debugowanie.
- Zaktualizuj package.json, aby dodać `jest-editor-support`.
- Zaktualizuj skrypt `start` w `package.json`, aby określić hosta.
- Dodaj `karma.conf.js` dla testów jednostkowych.
- Zaktualizuj `angular.json`, aby wskazać `karma.conf.js`.
- Dodaj projekt do rozwiązania.
- Zapisz ten plik.
