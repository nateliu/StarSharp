Dim FSO, TheExtension, CurrentPath
Dim TheFolder, Message, YesNo
Set FSO = CreateObject("Scripting.FileSystemObject")
'Set TheFolder = FSO.GetFolder("D:\Data-DMDevelopment\Nate_Modify\") 
CurrentPath = FSO.GetParentFolderName(WScript.ScriptFullName) 
Set TheFolder = FSO.GetFolder(CurrentPath)  

TheExtension = InputBox("Please type the extension you want to delete(excluding .), such as scc")
TheExtension = UCase(TheExtension)
Message = "Do you want to delete the extension of " & vbCrLf
Message = Message & vbCrLf & TheExtension &vbCrLf & "file?"
YesNo = MsgBox(Message, vbYesNo)
If YesNo = vbYes Then 
	WorkWithSubFolders TheFolder, TheExtension
	MsgBox "Delete finished. Press 'OK' or Enter to close this Dialog."
End If


Sub WorkWithSubFolders(ByVal AFolder, ByVal TheExtension)
	Dim MoreFolders, TempFolder	
	KillFilesWithExtensionIn AFolder, TheExtension
	Set MoreFolders = AFolder.SubFolders
	For Each TempFolder In MoreFolders
		WorkWithSubFolders TempFolder, TheExtension
	Next
End Sub

Sub KillFilesWithExtensionIn(AFolder,TheExtension)
	Dim AFile, TheFiles
	On Error Resume Next
	Set TheFiles = AFolder.Files
	For Each AFile In TheFiles
		If UCase(FSO.GetExtensionName(AFile.Path)) = TheExtension Then
			AFile.Delete
		End If
	Next
End Sub