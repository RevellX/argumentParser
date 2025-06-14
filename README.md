# 💻 Algorithm Practice CLI in C#

This is a simple C# console application designed for learning and practicing various algorithms. The program accepts command-line arguments to execute specific algorithm implementations (e.g., Fibonacci sequence, sorting algorithms, searching, etc.).

> 📘 It doesn't really do anything usefull, but it shows how my code looks.

---

## 🚀 Features

- Modular implementation of algorithms
- Easy-to-use CLI interface
- Great for testing and extending with new logic

---

## 🧠 Example Algorithms

Here is a list of currently included and planned algorithms:

- ✅ Fibonacci sequence
- ✅ Sorting (Bubble, Insertion, etc.)
- ❌ Factorial
- ❌ Prime number checker
- ❌ Search (Linear, Binary, etc.)

---

## 🛠 Usage

Accepted -action values:

- fibonacci
- sorting

### 🧾 Syntax

```bash
program.exe -action [algorithm]
program.exe -action [algorithm] -[algorith specific arguments and flags]
```

---

## 🔧 How to Build

1. Clone the repository:

```bash
git clone https://github.com/RevellX/argumentParser.git
cd argumentParser
```

2. Build the project:

```bash
dotnet build
```

3. Run with arguments:

```bash
dotnet run -action fibonacci -mode nthElement -n 20 -clearOutput
```
