# 🎓 AC-Info – Probleme de Algoritmică (Facultatea de Automatică și Calculatoare Iași)

Acest repository reprezintă o **platformă educațională de tip pbinfo**, adaptată pentru **nivel universitar**, dedicată studenților de la **Facultatea de Automatică și Calculatoare – Iași**.

Scopul proiectului este de a oferi un mediu organizat pentru:
- rezolvarea problemelor de algoritmică
- pregătirea pentru examene și colocvii
- aprofundarea structurilor de date și a tehnicilor avansate

### 📌 Cerințe preliminare

Înainte de a începe, asigură-te că ai instalate următoarele:

- .NET SDK (versiunea folosită de proiect)
- Docker Desktop
- SQL Server sau SQL Server Management Studio (SSMS)
- Git

---

## 1️⃣ Configurarea path-urilor locale (OBLIGATORIU)

Înainte de rularea API-ului, trebuie să modifici path-urile locale din fișierul:

Infrastructure/Repository/CompilerRepository.cs

go
Copiază codul

În funcția:

```csharp
private RunCResponse CompileCode(RunCompilerDTO runCDTO)
🔧 Modificări necesare
Fiecare utilizator trebuie să își seteze path-urile locale proprii, în funcție de structura folderelor de pe calculatorul său.

📁 Director pentru submissions
csharp
Copiază codul
// Exemplu laptop
// C:\\Users\\pc\\coding\\api_fac\\Proiect-Automatica-API\\Temp\\submissions\\

// Exemplu PC
// D:\\facultate\\ProjetFacult\\Temp\\submissions

var workDir = Path.Combine(
    "D:\\facultate\\ProjetFacult\\Temp\\submissions",
    submissionId.ToString()
);
👉 Înlocuiește path-ul cu unul valid de pe calculatorul tău.

📁 Director pentru CodeRunner (cpp)
csharp
Copiază codul
// Exemplu laptop
// C:\\Users\\pc\\coding\\api_fac\\Proiect-Automatica-API\\CodeRunner\\cpp\\

// Exemplu PC
// D:\\facultate\\ProjetFacult\\CodeRunner\\cpp

var runScriptSource = Path.Combine(
    "D:\\facultate\\ProjetFacult\\CodeRunner\\cpp",
    "run.sh"
);
👉 Acest path trebuie să ducă la folderul CodeRunner/cpp din proiect.

2️⃣ Configurarea bazei de date MSSQL
API-ul folosește o bază de date Microsoft SQL Server.

✔ Opțiuni acceptate:
SQL Server local

SQL Server prin Docker

SQL Server Management Studio (SSMS)

🔧 Connection String
Deschide fișierul:

pgsql
Copiază codul
appsettings.json
și modifică ConnectionStrings:

json
Copiază codul
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NumeBazaDate;Trusted_Connection=True;TrustServerCertificate=True;"
}
🔁 Înlocuiește:

Server – cu instanța ta SQL

Database – cu numele bazei tale de date

autentificarea, dacă folosești user/parolă

3️⃣ Construirea containerului Docker pentru C/C++
Pentru rularea codului C/C++, proiectul folosește un container Docker numit cpp-runner.

📂 Navighează în folderul:
bash
Copiază codul
CodeRunner/cpp/
🐳 Build imagine Docker
bash
Copiază codul
docker build -t cpp-runner .
▶ Rulare container (test)
bash
Copiază codul
docker run cpp-runner
📌 Notă: Docker Desktop trebuie să fie pornit.

4️⃣ Rularea API-ului
Din directorul principal al proiectului, rulează:

bash
Copiază codul
dotnet restore
dotnet run
API-ul va porni și va putea primi cereri pentru compilarea și rularea codului.

⚠ Probleme comune
❌ Docker nu pornește → verifică dacă Docker Desktop este instalat și pornit

❌ Erori de path → verifică dacă folderele există fizic pe disc

❌ Timeout la rulare → containerul Docker nu răspunde sau run.sh nu este copiat corect

✅ Concluzie
După parcurgerea tuturor pașilor:

API-ul va rula local

Codul C/C++ va fi compilat și executat în Docker

Output-ul va fi returnat corect către client
