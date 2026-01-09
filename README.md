# 🎓 AC-Info – Probleme de Algoritmică (Facultatea de Automatică și Calculatoare Iași)

Acest repository reprezintă o **platformă educațională de tip pbinfo**, adaptată pentru **nivel universitar**, dedicată studenților de la **Facultatea de Automatică și Calculatoare – Iași**.

Scopul proiectului este de a oferi un mediu organizat pentru:
- rezolvarea problemelor de algoritmică
- pregătirea pentru examene și colocvii
- aprofundarea structurilor de date și a tehnicilor avansate

==================================================

ACEST DOCUMENT EXPLICA PASII NECESARI PENTRU RULAREA
API-ULUI PE UN SISTEM LOCAL.

==================================================


CERINTE PRELIMINARE

- .NET SDK (versiunea folosita in proiect)
- Docker Desktop (instalat si pornit)
- SQL Server sau SQL Server Management Studio (SSMS)
- Git


==================================================
PASUL 1 - CONFIGURAREA PATH-URILOR LOCALE
==================================================

INAINTE DE RULAREA API-ULUI, ESTE OBLIGATORIU SA
MODIFICI PATH-URILE LOCALE DIN COD.

Fisier:
Infrastructure/Repository/CompilerRepository.cs

Functia:
private RunCResponse CompileCode(RunCompilerDTO runCDTO)


--------------------------------------------------
1.1 PATH PENTRU SUBMISSIONS
--------------------------------------------------

Cauta in cod:

var workDir = Path.Combine("...", submissionId.ToString());

Exemple de path-uri:

Laptop:
C:\Users\pc\coding\api_fac\Proiect-Automatica-API\Temp\submissions\

PC:
D:\facultate\ProjetFacult\Temp\submissions\

Exemplu corect:

var workDir = Path.Combine(
    "D:\\facultate\\ProjetFacult\\Temp\\submissions",
    submissionId.ToString()
);

IMPORTANT:
- Folderul trebuie sa existe pe disc
- API-ul trebuie sa aiba drepturi de scriere


--------------------------------------------------
1.2 PATH PENTRU CODERUNNER (CPP)
--------------------------------------------------

Cauta in cod:

var runScriptSource = Path.Combine("...", "run.sh");

Exemple de path-uri:

Laptop:
C:\Users\pc\coding\api_fac\Proiect-Automatica-API\CodeRunner\cpp\

PC:
D:\facultate\ProjetFacult\CodeRunner\cpp\

Exemplu corect:

var runScriptSource = Path.Combine(
    "D:\\facultate\\ProjetFacult\\CodeRunner\\cpp",
    "run.sh"
);


==================================================
PASUL 2 - CONFIGURAREA BAZEI DE DATE MSSQL
==================================================

API-ul foloseste o baza de date Microsoft SQL Server.

Poti folosi:
- SQL Server local
- SQL Server in Docker
- SQL Server Management Studio (SSMS)

--------------------------------------------------
2.1 MODIFICAREA CONNECTION STRING-ULUI
--------------------------------------------------

Fisier:
appsettings.json

Exemplu:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NumeBazaDate;Trusted_Connection=True;TrustServerCertificate=True;"
}

INLOCUIESTE:
- Server cu instanta ta SQL
- Database cu numele bazei tale de date
- Autentificarea daca folosesti user/parola


==================================================
PASUL 3 - CONFIGURAREA DOCKER PENTRU CPP
==================================================

Proiectul foloseste Docker pentru compilarea si
rularea codului C/C++.

--------------------------------------------------
3.1 NAVIGARE IN FOLDER
--------------------------------------------------

Deschide un terminal in:

CodeRunner/cpp/


--------------------------------------------------
3.2 BUILD IMAGINE DOCKER
--------------------------------------------------

Ruleaza comanda:

docker build -t cpp-runner .


--------------------------------------------------
3.3 TEST RULARE CONTAINER
--------------------------------------------------

Ruleaza comanda:

docker run cpp-runner

Daca nu apar erori, containerul este configurat corect.


==================================================
PASUL 4 - RULAREA API-ULUI
==================================================

Din folderul principal al proiectului ruleaza:

dotnet restore
dotnet run


==================================================
PROBLEME FRECVENTE
==================================================

- Docker nu porneste:
  Verifica daca Docker Desktop este instalat si pornit

- Erori de path:
  Verifica daca folderele exista fizic pe disc

- Timeout la rulare:
  Containerul Docker nu ruleaza sau scriptul run.sh
  nu este copiat corect


==================================================
FINAL
==================================================

Dupa parcurgerea tuturor pasilor:
- API-ul va rula local
- Codul C/C++ va fi compilat si rulat in Docker
- Output-ul va fi returnat corect

==================================================

