# Unity + GitHub
This document describes how to set up a Unity project and a GitHub repository to work together.

**1. Create the Unity project first.**

**2. Then, initialize the Git repository in the Unity project folder.**

`git init`

**3. Add a .gitignore file**

Example of where to get one: https://github.com/github/gitignore/blob/main/Unity.gitignore

**4. Make your first commit**

- Add all the relevant files (the ones not ignored by the `.gitignore`) to your staging area:
    
    `git add .`
    
- Commit the files:
    
    `git commit -m "Initial commit of Unity project"` 
    

**5. Create the Remote Repository on GitHub**

**Important:** Do NOT check the box to "Add a README file" or "Add a .gitignore". Your local project already has these.

- GitHub will give you a set of commands to link your local repo to this new remote one. Copy and paste the two commands into your terminal:
    
    `git remote add origin git@github.com:hpalo/Tic-Tac-Toe.git` Use here your own repository
    
    `git push -u origin main`
    

You have now successfully created the Unity project, set up a local Git repository, and pushed it.
