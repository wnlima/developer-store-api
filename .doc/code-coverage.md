# ✅ Code Coverage Guide

This guide explains how to generate a code coverage report for the solution using the provided `coverage-report.sh` script.

---

## 📦 Requirements

Before generating the coverage report, make sure the following are installed:

### ✅ Required Tools

| Tool | Installation Command |
|------|----------------------|
| [.NET SDK 8.0+](https://dotnet.microsoft.com/download) | — |
| `coverlet.console` | `dotnet tool install --global coverlet.console` |
| `reportgenerator` | `dotnet tool install --global dotnet-reportgenerator-globaltool` |
| Bash (e.g., Git Bash for Windows) | [Git for Windows](https://gitforwindows.org/) or a WSL shell |

---

## 📁 Script Location

The script is located at the root of the solution:

/coverage-report.sh

---

## 🚀 How to Run the Coverage Report

From the root of the repository, run:

```bash
./coverage-report.sh
```

💡 On Windows, use Git Bash or WSL if you’re not using a Unix shell.

⸻

🔍 What the Script Does
1.	Installs required tools (if missing)
2.	Cleans and builds the solution
3.	Runs all tests with code coverage enabled
4.	Excludes non-relevant files (e.g., Startup, Migrations, IoC, etc.)
5.	Generates a human-friendly HTML coverage report
6.	Outputs the final report at:

./TestResults/CoverageReport/index.html

⸻

📊 Coverage Output

The final HTML report will show:
	•	Line-by-line test coverage
	•	Per-project and per-file breakdown
	•	Highlighted uncovered lines

To view it, simply open the following file in your browser:

./TestResults/CoverageReport/index.html

⸻

🧹 Cleanup

The script also removes temporary files (bin, obj) after generating the report.

⸻

💡 Notes
	•	This report includes only application code, excluding:
	•	Program.cs, Startup.cs
	•	Migrations
	•	IoC and test utility classes
	•	Make sure your tests are up to date and relevant before running the report
	•	Reports follow the Cobertura XML format for compatibility

⸻

🧪 CI Integration (Optional)

For CI pipelines, the same logic can be reused in GitHub Actions or other CI tools using the commands inside this script.