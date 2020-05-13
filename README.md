# Github Users 
Aplikacja została stworzona z myślą o rekrutacji do firmy **Allegro**. 
Stos technologiczny:

 - .NET Core 3.1
 - Autofac
 - Automapper
 - NBomber
 
# Wdrożenie
W celu wdrożenia aplikacji, deploy należy wykonać na środowisku z zainstalowanym Dockerem.
Oto komendy, które umożliwią wdrożenie aplikacji:
 
- docker pull kroliczek94/github-users:1.0.0
- docker run --rm -it -p 8001:80 --detach kroliczek94/github-users:1.0.0

W przypadku, gdyby pojawiał się błąd unauthorized, należy w pliku appsettings.json wprowadzić prawidłowy token autoryzacyjny do API githuba.

# Optymalizacje
W celu poprawienia wydajności zapytań, wprowadziłem 2 optymalizacje
-  każdy reqeust do endpointa /repositories/{owner} jest cachowany przez ASP.NET (przez 10 sekund), przez co dane są zwracane automatycznie w przypadku zapytania o tego samego użytkownika
-  mając na uwadze, że autoryzowany użytkownik może wykonać zaledwie 6000 requestów/godzinę, dane o pobranych użytkownikach są przechowywane w Cachu aplikacji. Dzięki temu, wysyłając ponowny request o danego użytkownika, podajemy ETag wcześniejszego zapytania - jeśli jest taki sam (czyli użytkownik nie został zmodyfikowany), dostajemy status (NOT_MODIFIED), a nasz licznik requestów (rate limit) nie zostaje zinkrementowany. Samo pobranie też jest dzięki temu szybsze.

# Testy
Aby przetestować wydajność API, zostały zaimplementowane testy obciążeniowe. Skorzystałem z biblioteki NBomber - posiadała ona proste API oraz możliwość łatwych zmian w strukturze zapytań. Aby nie pobierać ciągle informacji o tym samym użytkowniku, skorzystałem z listy 960 najaktywniejszych użytkowników Githuba. Testy spokojnie osiągają zadany minimalny limit 20 requestów/sekundę. Dzięki testom mogłem również dojrzeć, że wydajność testów znacznie się poprawia, dzięki zastosowanym optymalizacjom.
