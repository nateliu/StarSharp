Dim FSO, CurrentPath
Dim TheFolder, Message, YesNo
Set FSO = CreateObject("Scripting.FileSystemObject")
CurrentPath = FSO.GetParentFolderName(WScript.ScriptFullName) 
Set TheFolder = FSO.GetFolder(CurrentPath)  

Message = "Are you sure you want to delete all bin/obj folders and all of it contain files?" & vbCrLf
YesNo = MsgBox(Message, vbYesNo)
If YesNo = vbYes Then 
	WorkWithSubFolders TheFolder
	MsgBox "Delete finished. Press 'OK' or Enter to close this Dialog."
End If

Sub WorkWithSubFolders(ByVal AFolder)
	On Error Resume Next
	Dim MoreFolders, TempFolder	
	KillFilesWithExtensionIn AFolder
	Set MoreFolders = AFolder.SubFolders
	For Each TempFolder In MoreFolders
		WorkWithSubFolders TempFolder
	Next
End Sub

Sub KillFilesWithExtensionIn(ByVal AFolder)
	On Error Resume Next		
	If UCase(AFolder.Name)="BIN" Or UCase(AFolder.Name)="OBJ" Then
		AFolder.Delete
	End If
End Sub
