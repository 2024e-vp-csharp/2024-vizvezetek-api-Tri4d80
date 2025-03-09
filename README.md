[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/gTJnpJ5c)
# Vízvezetékszerelő munkalapok #

A sárgahegyi Tiszta Víz Kft. hosszú ideje áll a lakosság szolgálatában.
Munkatársai az építkezéseken végzett munka mellett bármilyen problémát
gyorsan orvosolnak, legyen szó csöpögő csapról vagy csőtörésről.

A cégnek szüksége van egy Web API alkalmazásra, ami az adatbázisban
szereplő munkákat adja ki a megfelelő formátumban.

**0.)** Töltse fel a MySQL adatbázist a mellékelt *vizvezetek.sql* fájl
alapján. Nyissa meg a *Vizvezetek.API* ASP.NET Core Web API projektet!

**1.)** Az alkalmazás üzemeltetője szeretné majd a későbbiekben
áttelepíteni az adatbázist, így a kapcsolati adatok
módosulnak. Készítse fel az alkalmazást, hogy az adatbázis kapcsolódási
adatokat egy konfigurációs fájlból olvassa be!

**2.)** Az alkalmazás az *api/Munkalapok* erőforráson kell, hogy
kommunikáljon.

**3.)** Fontos formai megkötés, hogy a munkalapokat majd az alábbi
formátumban kell szolgáltatnia a Web API-nak:

```json
{
  "id": 1,
  "beadas_datum": "2001-05-28T00:00:00",
  "javitas_datum": "2001-05-31T00:00:00",
  "helyszin": "Kőváros, Harcsa u. 99.",
  "szerelo": "Szabó György",
  "munkaora": 2,
  "anyagar": 3721
}
```

A formátum átalakításához használjon egy segéd osztályt és tegye a
*DTOs* mappába. A helyszín mező a *'telepules (vessző) utca'* formátumot
adja vissza. Dátum mező esetén írja át *DateTime* típusra *DateOnly* helyett, a helyes működéshez.

**4.)** Hozzon létre egy paraméter nélküli végpontot az *api/Munkalapok útvonalon*, amit HTTP GET
metódussal lehet elérni. A végpont adja vissza az
összes adatot a munkalapon, a megszabott JSON formátumban.

**5.)** Hozzon létre egy másik GET végpontot, ami a munkalap azonosítót kapja URL-ben,és adja vissza a talált rekordot. Az útvonal az 5-ös azonosító esetén így épül fel: *api/Munkalapok/5*.

**6.)** Készítsen egy másik osztályt a *DTOs* mappában
*MunkalapKeresesDto* néven. Az osztálynak két tulajdonság értéke legyen:
helyszín azonosító, és szerelő azonosító.

**7.)** Hozzon létre egy POST végpontot a kereséshez.
Paraméterként kapja meg a *MunkalapKeresesDto* objektumot és keresse
ki, hogy a kapott helyszín és szerelő együtt milyen munkalapokon
szerepel.

**8.)** Egészítse ki az *api/Munkalapok* lekérdezést opcionális évszám
kereséssel a lezárt munkalapokra. Például az *api/Munkalapok/ev/2002* adja
vissza a 2002-ben lezárt munkalapok listáját.
