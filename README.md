
# Terminal Runner

This project is written in C# and uses the Avalonia UI framework. Users can input terminal/shell commands, execute them, and view the output in a color-coded textbox output.

📌 Features
* Run terminal commands with arguments.
* Display stdout and stderr separately with different colors(green for success, red for failure).
* Highlight exit codes to indicate command success or failure.

🧪 Testing
* All tests are written in a separate project
* Integration tests are there to ensure that terminal works properly on the underlying platform (it's been tested for Windows only).
* To run the tests, run the following command:
    ```bash
    dotnet test Project.Tests
    ```


