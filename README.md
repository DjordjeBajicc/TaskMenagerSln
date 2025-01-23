# TaskManager

## Uvod

TaskManager je jednostavna aplikacija koja vam omogućava efikasno praćenje i upravljanje vašim zadacima. Aplikacija je razvijena da bi pojednostavila proces praćenja obaveza koje trebamo obaviti kao i praćenja rokova samih obaveza.Riječ je o desktop aplikaciji razvijenoj u C# programskom jeziku uz korištenje WPF (Windows Presentation Foundation) razvojnog okvira. Aplikacija koristi MySQL bazu podataka za čuvanje svih podataka. Za upotrebu aplikacije potreban je desktop ili laptop računar sa Windows operativnim sistemom i minimalnim hardverskim specifikacijama.  

## Korisničko uputstvo

Pri pokretanju aplikacije korisnik odmah može započeti sa radom odnosno upravljanjem svojih zadataka jer je ekran koji mu se prikazuje zapravo i glavni ekran same aplikacije.

![image](https://github.com/user-attachments/assets/87376de5-051c-4def-ab66-f3d8f83ec571)

Na ekranu vidimo tri liste pri čemu je prva lista rezervisana za zadatke koje tek trebamo obaviti nekada u budućnosti, druga lista za zadatke koje trenutno obavljamo te treća lista koja je rezervisana za zadatke koje smo obavili. Zadatke prenosimo iz jedne u drugu listu tako što jednostavno zadržimo kursor nad nazivom zadatka i ne puštajući lijevi taster miša prevučemo zadatak u drugu listu. Dvoklikom na naziv zadatka nam se otvara modal u kom možemo vidjeti sam opis zadatka koji možemo i izmjenizi te rok do kog je potrebno da obavimo taj zadatak.

![image](https://github.com/user-attachments/assets/19a0b80a-735e-4581-9392-670f8876bdc5)

Takođe u gornjem desnom dijelu ekrana vidimo dugme čijim pritiskom nam se otvara modal u kom kreiramo novi zadatak unoseci naziv, opis, te rok za završetak tog zadatka.

![image](https://github.com/user-attachments/assets/63bb29e6-7a5b-409b-8380-38b713bee1a8)

Svaki zadatak je moguće obrisati odnosno ukloniti iz bilo koje liste jednostavnim postupkom kojim prebacujemo zadatke između listi samo što u ovom slučaju zadatak prevučemo u crveni pravougaonik u gornjem desnom dijelu prozora. Nakon što smo zadatak prevukli u pomenuti pravougaonik, ispisuje nam se poruka o uspješnosti brisanja zadatka.

![image](https://github.com/user-attachments/assets/70d4c8a3-9836-4b2b-97a5-ffab0959de77)





