Unity Editor Git Commit
Author: Brett Cunningham
GitHub: hisnameisbrett
Date: 01/22/2019

Brief:
    If your Unity project is being tracked by git, this allows you to 
    quickly commit and push to your repo via a Unity editor window.
    Place the "commit.bat" script in the root of your project directory
    and the "EditorGitCommit.cs" under "Assets/Editor/". 

    When you want commit changes, within Unity go to Tools>Commit
    to open the window. Enter a commit message and press the "Commit" button.
    You should see the CMD process start and log the output. 
    
NOTES:
    -   If you want to store the "commit.bat" script elsewhere, just amend the
        path string at line 41 of "EditorGitCommit.cs":
            System.Diagnostics.Process.Start("commit.bat", string.Format("\"{0}\"", _message));
            
    -   If you want the CMD process to quit when its done automatically instead
        of waiting for input, just remove the last line of "commit.bat":
            set /p=Press any key to exit...
