# C# Find Command Reimplementation

This project is a C# console application that reimplements the `find` command, which is typically used in Unix-based systems. The goal of this project is to provide a similar functionality using .NET 9 in a console application, allowing users to search for files and directories based on specified conditions.

## Features

- Search for files and directories based on name or pattern.
- Ability to search recursively through subdirectories.
- Supports various options for filtering and displaying results.

## Usage

    Searches for a text string in a file or files.
    
    FIND [/V] [/C] [/N] [/I] [/OFF[LINE]] ""string"" [[drive:][path]filename[ ...]]
    
      /V         Displays all lines NOT containing the specified string.
      /C         Displays only the count of lines containing the string.
      /N         Displays line numbers with the displayed lines.
      /I         Ignores the case of characters when searching for the string.
      /OFF[LINE] Do not skip files with offline attribute set.
      ""string""   Specifies the text string to find.
      [drive:][path]filename
                 Specifies a file or files to search.
    
    If a path is not specified, FIND searches the text typed at the prompt
    or piped from another command.

    // === Example
    FIND /N /I "sherlock" Data/text/*.txt      // Display files ignoring case and show line numbers containing word "sherlock" under the folder Data/text.
    FIND /C "sherlock" Data/text/*.txt         // Display total count of lines of per file under the folder Data/text.
    FIND /I "sherlock" Data/text/alls-well-that-ends-well_TXT_FolgerShakespeare.txt  // Display all lines not containing "sherlock" in the file. 

## Requirements

- .NET 9 SDK installed on your machine.
- A command-line interface (CLI) such as Command Prompt or Terminal.

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/find-command-reimplementation.git

2. Navigate to the project directory:

   ```bash
   cd _PROJECT_KFind

3. Build the project using commandline or VS2022

   ```bash
   dotnet build
   
