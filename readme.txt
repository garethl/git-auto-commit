git-auto-commit is a tiny C#/.net 4 app that sits in your system tray, 
watching folders you specify. When a file is changed or added, it waits 
a specified length of time, then automatically commits the changed files 
to a git repository.

git-auto-commit is copyright (c) 2011 Gareth Lennox (garethl@dwakn.com)
All rights reserved.

git-auto-commit is licensed under the BSD license. See license.txt for 
more information. 

usage: 
    git-auto-commit <commit-interval> <dir 1>, <dir 2>, ..., <dir n>

where:
    commit-interval: commit interval in seconds
    dir1,2,n:        directories to watch

Note that git.exe is assumed to be in the path and that all directories are
already git repositories