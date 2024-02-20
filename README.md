### MyMauiApp

Repozytorium zawiera prostą aplikację zbudowaną przy użyciu platformy .NET MAUI (Multi-platform App UI). Aplikacja umożliwia zarządzanie listą uczniów, ich klasami oraz numerami w dzienniku. Użytkownicy mogą dodawać, edytować, usuwać uczniów oraz losowo wybierać ucznia z określonej klasy.

#### Struktura Projektu

Projekt składa się z następujących komponentów:

1. **Modele**:
   - `Person.cs`: Definiuje klasę `Person` z właściwościami dla imienia, klasy, numeru w dzienniku, obecności oraz statusu zapytania.

2. **Widoki**:
   - `MainPage.xaml`: Definiuje główną stronę aplikacji. Umożliwia użytkownikom wprowadzanie danych ucznia, zapisywanie, wczytywanie oraz losowe wybieranie ucznia.
   - `StudentsPage.xaml`: Wyświetla listę uczniów i umożliwia edycję lub usuwanie ich.
   - `EditStudentPage.xaml`: Pozwala użytkownikom edytować dane ucznia.

3. **Kod-behind**:
   - `MainPage.xaml.cs`, `StudentsPage.xaml.cs`, `EditStudentPage.xaml.cs`: Zawiera logikę obsługi interakcji użytkownika i zarządzania danymi uczniów.

4. **Style**:
   - `App.xaml`: Zawiera globalne style aplikacji.

#### Funkcjonalność

- **Strona Główna (MainPage.xaml)**:
  - Użytkownicy mogą wprowadzać dane ucznia, takie jak imię, klasa i numer w dzienniku.
  - Mogą zapisać informacje o uczniu, wczytać listę uczniów i wybrać losowego ucznia.
  - Strona wyświetla listę klas do wyboru oraz przyciski do różnych akcji.

- **Strona Uczniów (StudentsPage.xaml)**:
  - Wyświetla listę uczniów pogrupowanych według klasy.
  - Użytkownicy mogą wybrać klasę, aby zobaczyć uczniów z tej klasy.
  - Mogą edytować lub usuwać poszczególnych uczniów.

- **Strona Edycji Ucznia (EditStudentPage.xaml)**:
  - Pozwala użytkownikom edytować dane ucznia, takie jak imię, klasa i numer w dzienniku.

#### Stylowanie

- Interfejs aplikacji jest stylizowany przy użyciu składni przypominającej CSS, która definiuje wygląd różnych elementów, takich jak pola tekstowe, przyciski i listy.
- Stylowanie jest stosowane za pomocą selektorów kierujących się do konkretnych elementów lub klas w znacznikach XAML.

#### Użycie

Aby skorzystać z aplikacji:

1. Sklonuj to repozytorium na swój lokalny komputer.
2. Utwórz plik people.txt na twoim Pulpicie.
3. Otwórz rozwiązanie w programie Visual Studio lub innym kompatybilnym środowisku.
4. Zbuduj i uruchom aplikację na wybranej platformie.

#### Użyte Technologie

- .NET MAUI
- C#
- XAML
