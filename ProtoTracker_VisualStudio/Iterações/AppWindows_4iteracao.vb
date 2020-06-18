Imports MySql.Data.MySqlClient
Public Class ProtoTracker

    Dim CurWidth As Integer = Me.Width
    Dim CurHeight As Integer = Me.Height
    Dim connection As New MySqlConnection("server=den1.mysql4.gear.host;Port=3306;database=projectpial;username=projectpial;password=Uh4r?SyJV85~")
	
	Private Sub btnCadastro_Click(sender As Object, e As EventArgs) Handles btnCadastro.Click
        currentOption.Height = btnCadastro.Height
        currentOption.Top = btnCadastro.Top
        PanelCadastro.Visible = True
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelRemove.Visible = False

        TagBox.Text = ""
        NomeBox.Text = ""
        IdBox.Text = ""
        ProjetoBox.Text = ""
        Andar.SelectedIndex = -1
        Edif.SelectedIndex = -1


    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        currentOption.Height = btnUpdate.Height
        currentOption.Top = btnUpdate.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = True
        PanelSearch.Visible = False
        PanelRemove.Visible = False

        IDAtualizarBox.Text = ""
        CodTagBox.Text = ""
        Edif2.SelectedIndex = -1
        Andar2.SelectedIndex = -1

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        currentOption.Height = btnSearch.Height
        currentOption.Top = btnSearch.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = True
        PanelRemove.Visible = False

        CodTagSearchBox.Text = ""
        IDSearchTextBox.Text = ""
        Edif1.SelectedIndex = -1
        Andar1.SelectedIndex = -1

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        currentOption.Height = btnRemove.Height
        currentOption.Top = btnRemove.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = True

        TextBoxRemoveID.Text = ""
        TextBoxRemoveTag.Text = ""

    End Sub
	
	Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        Dim RatioHeight As Double = (Me.Height - CurHeight) / CurHeight
        Dim RatioWidth As Double = (Me.Width - CurWidth) / CurWidth

        For Each Ctrl As Control In Controls
            Ctrl.Width += Ctrl.Width * RatioWidth
            Ctrl.Left += Ctrl.Left * RatioWidth
            Ctrl.Top += Ctrl.Top * RatioHeight
            Ctrl.Height += Ctrl.Height * RatioHeight
        Next

        CurHeight = Me.Height
        CurWidth = Me.Width

    End Sub
	
	Private Sub btnRegUtil_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        PanelLogin.Visible = False
        PanelRegisto.Visible = True
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = False

        NomeUtilBox.Text = ""
        PassBox.Text = ""
        EmailUtilBox.Text = ""
        TelemovelBox.Text = ""

    End Sub

    Private Sub btnRegistUtil_click(sender As Object, e As EventArgs) Handles Registar_btn.Click

        Dim command As New MySqlCommand("SELECT * FROM User where email = @mail", connection)

        command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = EmailUtilBox.Text

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If NomeUtilBox.Text = "" Or PassBox.Text = "" Or EmailUtilBox.Text = "" Then

            MessageBox.Show("Por favor preencha todos os campos necessários!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = True
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table.Rows.Count() > 0 Then

            MessageBox.Show("Email já registado!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = True
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        Else

            Dim command2 As New MySqlCommand("INSERT INTO `User`(`name`,`password`,`email`,`phone`,`access`) VALUES (@nm,@pass,@mail,@tel,@acc)", connection)

            command2.Parameters.Add("nm", MySqlDbType.VarChar).Value = NomeUtilBox.Text
            command2.Parameters.Add("pass", MySqlDbType.VarChar).Value = PassBox.Text
            command2.Parameters.Add("mail", MySqlDbType.VarChar).Value = EmailUtilBox.Text
            command2.Parameters.Add("tel", MySqlDbType.VarChar).Value = TelemovelBox.Text
            If admin.Checked Then
                command2.Parameters.Add("acc", MySqlDbType.VarChar).Value = "1"
            Else
                command2.Parameters.Add("acc", MySqlDbType.VarChar).Value = "0"
            End If

            connection.Open()

            If command2.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Utilizador registado com sucesso!")
                TextBoxEmail.Text = EmailUtilBox.Text
                TextBoxPassword.Text = ""
            Else
                MessageBox.Show("Erro no registo")
            End If

            connection.Close()

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        End If

        NomeUtilBox.Text = ""
        PassBox.Text = ""
        EmailUtilBox.Text = ""
        TelemovelBox.Text = ""

    End Sub
	
	Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim command As New MySqlCommand("SELECT * FROM User where email = @mail", connection)

        command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = TextBoxEmail.Text

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If TextBoxEmail.Text = "" And TextBoxPassword.Text = "" Then

            Dim iExit As DialogResult

            iExit = MessageBox.Show("Introduza Email e Password!")

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False


        ElseIf table.Rows.Count = 0 Then

            MessageBox.Show("Utilizador não Registado!")

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table.Rows(0)(1).ToString() = TextBoxPassword.Text Then

            If table.Rows(0)(4) = "1" Then
                PanelLogin.Visible = False
                PanelRegisto.Visible = False
                PanelCadastro.Visible = True
                PanelUpdate.Visible = False
                PanelSearch.Visible = False
                PanelMostrarLoc.Visible = False
                PanelRemove.Visible = False
                PanelLeftBlock.Visible = False
                PanelLeft.Visible = True

            Else

                PanelLogin.Visible = False
                PanelRegisto.Visible = False
                PanelCadastro.Visible = False
                PanelUpdate.Visible = True
                PanelSearch.Visible = False
                PanelMostrarLoc.Visible = False
                PanelRemove.Visible = False
                PanelLeftBlock.Visible = True
                PanelLeft.Visible = False

            End If

        Else

            MessageBox.Show("Password Incorreta!")

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub VoltarRegisto(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PanelLogin.Visible = True
        PanelRegisto.Visible = False
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = False
    End Sub
	
	Private Sub FecharAPP(sender As Object, e As EventArgs) Handles PictureBox9.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub
	
	Private Sub FecharAppReg(sender As Object, e As EventArgs) Handles PictureBox10.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = True
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub

    Private Sub FecharAppCad(sender As Object, e As EventArgs) Handles PictureBox11.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub

    Private Sub FecharAppUpdt(sender As Object, e As EventArgs) Handles PictureBox12.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = True
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub

    Private Sub FecharAppProc(sender As Object, e As EventArgs) Handles PictureBox13.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = True
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub

    Private Sub FecharAppRem(sender As Object, e As EventArgs) Handles PictureBox14.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = True
        End If

    End Sub
	
	Private Sub Close_btn_hist(sender As Object, e As EventArgs) Handles PictureBox18.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelMostrarProt.Visible = True
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub close_app_Loc(sender As Object, e As EventArgs) Handles PictureBox15.Click
        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
            PanelMostrarLoc.Visible = True
            PanelMostrarProt.Visible = False
        End If
    End Sub
	
	Private Sub btnBack_click(sender As Object, e As EventArgs) Handles PictureBox7.Click

        Dim SairSessão As New DialogResult
        SairSessão = MessageBox.Show("Deseja terminar sessao?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairSessão = DialogResult.Yes Then
            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

            TextBoxEmail.Text = ""
            TextBoxPassword.Text = ""

        End If

        If SairSessão = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub
	
	Private Sub btnCadastrar_click(sender As Object, e As EventArgs) Handles btnConfirmRegister.Click

        Dim Location As String
        Location = "Building " & Edif.SelectedItem & ", Floor " & Andar.SelectedItem

        Dim command1 As New MySqlCommand("SELECT * FROM Prototype where tag_code = @cod_tag", connection)

        command1.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = TagBox.Text

        Dim adapter As New MySqlDataAdapter(command1)
        Dim table As New DataTable()
        adapter.Fill(table)

        Dim command3 As New MySqlCommand("SELECT * FROM Prototype where prototype_id = @id", connection)

        command3.Parameters.Add("@id", MySqlDbType.VarChar).Value = IdBox.Text

        Dim adapter1 As New MySqlDataAdapter(command3)
        Dim table1 As New DataTable()
        adapter1.Fill(table1)

        If TagBox.Text = "" Or NomeBox.Text = "" Or IdBox.Text = "" Or Location = "" Or (Edif.SelectedIndex < 0 Or Andar.SelectedIndex < 0) Then

            MessageBox.Show("Por favor preencha todos os campos necessários!")
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table.Rows.Count() > 0 Then

            MessageBox.Show("Código Tag já associado a um protótipo!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table1.Rows.Count() > 0 Then

            MessageBox.Show("Id já associado a um protótipo!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False


        Else

            Dim Command As New MySqlCommand("INSERT INTO `Prototype`(`tag_code`,`name`,`prototype_id`,`project`,`location`,`name_reg`,`date_reg`,`device`) VALUES (@cod,@nome,@id,@proj,@loc,@Rg_mail,@Rg_data,@dev)", connection)

            Command.Parameters.Add("cod", MySqlDbType.VarChar).Value = TagBox.Text
            Command.Parameters.Add("nome", MySqlDbType.VarChar).Value = NomeBox.Text
            Command.Parameters.Add("id", MySqlDbType.VarChar).Value = IdBox.Text
            Command.Parameters.Add("proj", MySqlDbType.VarChar).Value = ProjetoBox.Text
            Command.Parameters.Add("loc", MySqlDbType.VarChar).Value = Location

            Command.Parameters.Add("Rg_mail", MySqlDbType.VarChar).Value = TextBoxEmail.Text
            Command.Parameters.Add("Rg_data", MySqlDbType.DateTime).Value = Now
            Command.Parameters.Add("dev", MySqlDbType.VarChar).Value = "Aplicação Windows"

            Dim command2 As New MySqlCommand("INSERT INTO `History`(`date`,`tag_code`,`location`) VALUES (@data,@tag,@loc2)", connection)

            command2.Parameters.Add("data", MySqlDbType.DateTime).Value = Now
            command2.Parameters.Add("tag", MySqlDbType.VarChar).Value = TagBox.Text
            command2.Parameters.Add("loc2", MySqlDbType.VarChar).Value = Location

            connection.Open()

            If Command.ExecuteNonQuery() = 1 And command2.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Protótipo cadastrado com sucesso!")

                TagBox.Text = ""
                NomeBox.Text = ""
                IdBox.Text = ""
                ProjetoBox.Text = ""
                Andar.SelectedIndex = -1
                Edif.SelectedIndex = -1
            Else
                MessageBox.Show("Erro no cadastro!")
            End If

            connection.Close()

            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub btnAtualizar(sender As Object, e As EventArgs) Handles btnConfirmUpdate.Click


        If CodTagBox.Text = "" Or IDAtualizarBox.Text = "" Then

            MessageBox.Show("Por favor preencha os campos Código Tag e ID!")

        ElseIf Edif2.SelectedIndex < 0 Or Andar2.SelectedIndex < 0 Then

            MessageBox.Show("Preencha a Localização!")

        Else

            Dim command1 As New MySqlCommand("SELECT * FROM Prototype where tag_code = @cod_tag And prototype_id = @id", connection)

            command1.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = CodTagBox.Text
            command1.Parameters.Add("@id", MySqlDbType.VarChar).Value = IDAtualizarBox.Text

            Dim adapter1 As New MySqlDataAdapter(command1)
            Dim table1 As New DataTable()
            adapter1.Fill(table1)

            Dim Location2 As String
            Location2 = "Building " & Edif2.SelectedItem & ", Floor " & Andar2.SelectedItem

            If table1.Rows.Count() = 0 Then

                MessageBox.Show("Nenhum protótipo encontrado com o Código Tag e ID introduzido!")

            Else

                Dim command3 As New MySqlCommand("UPDATE `prototype` SET `location`=@loc, `device`=@dev WHERE `tag_code`=@tag", connection)

                command3.Parameters.Add("@tag", MySqlDbType.VarChar).Value = CodTagBox.Text
                command3.Parameters.Add("@loc", MySqlDbType.VarChar).Value = Location2
                command3.Parameters.Add("dev", MySqlDbType.VarChar).Value = "Aplicação Windows"

                connection.Open()
                If command3.ExecuteNonQuery() Then

                    MessageBox.Show("Localização atualizada com sucesso!")

                End If
                connection.Close()

                Dim command4 As New MySqlCommand("INSERT INTO `History`(`date`,`tag_code`,`location`) VALUES (@data,@tag2,@loc2)", connection)

                command4.Parameters.Add("data", MySqlDbType.DateTime).Value = Now
                command4.Parameters.Add("tag2", MySqlDbType.VarChar).Value = CodTagBox.Text
                command4.Parameters.Add("loc2", MySqlDbType.VarChar).Value = Location2

                connection.Open()
                If command4.ExecuteNonQuery() Then
                    CodTagBox.Text = ""
                    IDAtualizarBox.Text = ""
                    Edif2.SelectedIndex = -1
                    Andar2.SelectedIndex = -1
                End If
                connection.Close()

            End If

        End If

        PanelLogin.Visible = False
        PanelRegisto.Visible = False
        PanelCadastro.Visible = False
        PanelUpdate.Visible = True
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = False

    End Sub
	
	Private Sub btn_update_block(sender As Object, e As EventArgs) Handles Button1.Click
        Panel2.Height = Button1.Height
        Panel2.Top = Button1.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = True
        PanelSearch.Visible = False
        PanelRemove.Visible = False
    End Sub
	
	Private Sub btn_backblock(sender As Object, e As EventArgs) Handles PictureBox17.Click

        Dim SairSessão As New DialogResult
        SairSessão = MessageBox.Show("Deseja terminar sessao?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairSessão = DialogResult.Yes Then
            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

            TextBoxEmail.Text = ""
            TextBoxPassword.Text = ""

        End If

        If SairSessão = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = True
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub ProcLoc_clibk(sender As Object, e As EventArgs) Handles btnConfirmSearch.Click



        If (CodTagSearchBox.Text = "" And IDSearchTextBox.Text = "") And (Edif1.SelectedIndex < 0 And Andar1.SelectedIndex < 0) Then

            MessageBox.Show("Por favor preencha algum dos campos!")

        ElseIf (CodTagSearchBox.Text <> "" And IDSearchTextBox.Text <> "") And (Edif1.SelectedIndex >= 0 And Andar1.SelectedIndex >= 0) Then

            MessageBox.Show("Por favor opte por uma das formas de procura!")

        ElseIf (CodTagSearchBox.Text = "" Or IDSearchTextBox.Text = "") And (Edif1.SelectedIndex < 0 And Andar1.SelectedIndex < 0) Then

            MessageBox.Show("Por favor preencha os campos Código Tag e ID!")

        ElseIf (CodTagSearchBox.Text = "" And IDSearchTextBox.Text = "") And (Edif1.SelectedIndex < 0 Or Andar1.SelectedIndex < 0) Then

            MessageBox.Show("Por favor preencha Edifício e Andar!")

        ElseIf (CodTagSearchBox.Text = "" Or IDSearchTextBox.Text = "") And (Edif1.SelectedIndex < 0 Or Andar1.SelectedIndex < 0) Then

            MessageBox.Show("Por favor opte por uma das formas de procura!")

        Else
            Dim Location_proc As String
            Location_proc = "Building " & Edif1.SelectedItem & ", Floor " & Andar1.SelectedItem

            Dim command1 As New MySqlCommand("SELECT * FROM Prototype where tag_code = @cod_tag and prototype_id = @id", connection)

            command1.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = CodTagSearchBox.Text
            command1.Parameters.Add("@id", MySqlDbType.VarChar).Value = IDSearchTextBox.Text

            Dim adapter As New MySqlDataAdapter(command1)
            Dim table As New DataTable()
            adapter.Fill(table)

            If table.Rows.Count = 0 And (Edif1.SelectedIndex >= 0 And Andar1.SelectedIndex >= 0) Then

                Dim command3 As New MySqlCommand("SELECT name, tag_code, prototype_id FROM prototype where location = @loc_proc", connection)

                command3.Parameters.Add("@loc_proc", MySqlDbType.VarChar).Value = Location_proc

                Dim adapter3 As New MySqlDataAdapter(command3)
                Dim table3 As New DataTable()
                adapter3.Fill(table3)

                If table3.Rows.Count = 0 Then

                    MessageBox.Show("Nenhum protótipo encontrado com a localização pedida!")

                Else

                    titulo2.Visible = False
                    titulo.Location = New Point(50, 125)
                    titulo.TextAlign = HorizontalAlignment.Center
                    titulo.Size = New Point(500, 30)
                    titulo.Text = "Resultado da pesquisa por Edifício " & Edif1.SelectedItem & " Andar " & Andar1.SelectedItem

                    DataGridView1.Location = titulo.Location + New Point(0, 35)
                    DataGridView1.DataSource = table3
                    PictureBox22.Visible = False

                    PanelLogin.Visible = False
                    PanelRegisto.Visible = False
                    PanelCadastro.Visible = False
                    PanelUpdate.Visible = False
                    PanelSearch.Visible = False
                    PanelMostrarLoc.Visible = False
                    PanelMostrarProt.Visible = True
                    PanelRemove.Visible = False

                End If

            ElseIf table.Rows.Count = 0 And (Edif1.SelectedIndex < 0 And Andar1.SelectedIndex < 0) Then

                MessageBox.Show("Nenhum protótipo encontrado com Código Tag e ID introduzidos!")

            Else

                If table.Rows(0)(4).ToString() = "OUTSIDE" Then

                    Cod_tag_Box.Text = table.Rows(0)(0).ToString
                    Nome_Box.Text = table.Rows(0)(1).ToString
                    ID_box.Text = table.Rows(0)(2).ToString
                    Local_Atual_box.Text = "Desconhecida"
                    reg_prot.Text = table.Rows(0)(5).ToString & " " & table.Rows(0)(6).ToString
                    proj_prot.Text = table.Rows(0)(3).ToString
                Else
                    Cod_tag_Box.Text = table.Rows(0)(0).ToString
                    Nome_Box.Text = table.Rows(0)(1).ToString
                    ID_box.Text = table.Rows(0)(2).ToString
                    Local_Atual_box.Text = table.Rows(0)(4).ToString
                    reg_prot.Text = table.Rows(0)(5).ToString & " " & table.Rows(0)(6).ToString
                    proj_prot.Text = table.Rows(0)(3).ToString
                End If

                PanelLogin.Visible = False
                PanelRegisto.Visible = False
                PanelCadastro.Visible = False
                PanelUpdate.Visible = False
                PanelSearch.Visible = False
                PanelMostrarLoc.Visible = True
                PanelMostrarProt.Visible = False
                PanelRemove.Visible = False


            End If


        End If

        CodTagSearchBox.Text = ""
        IDSearchTextBox.Text = ""
        Edif1.SelectedIndex = -1
        Andar1.SelectedIndex = -1
    End Sub
	
	Private Sub btn_proc_block(sender As Object, e As EventArgs) Handles Button2.Click
        Panel2.Height = Button2.Height
        Panel2.Top = Button2.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = True
        PanelRemove.Visible = False
    End Sub
	
	Private Sub History_btn_Click(sender As Object, e As EventArgs) Handles History_btn.Click

        Dim Command As New MySqlCommand("SELECT location, date FROM history where tag_code = @tag_cod order by date desc", connection)

        Command.Parameters.Add("@tag_cod", MySqlDbType.VarChar).Value = Cod_tag_Box.Text

        Dim adapter As New MySqlDataAdapter(Command)
        Dim table As New DataTable()
        adapter.Fill(table)

        DataGridView1.DataSource = table

        Dim command2 As New MySqlCommand("SELECT * FROM prototype where tag_code = @tag_code", connection)

        command2.Parameters.Add("@tag_code", MySqlDbType.VarChar).Value = Cod_tag_Box.Text
        Dim adapter2 As New MySqlDataAdapter(command2)
        Dim table2 As New DataTable()
        adapter2.Fill(table2)

        DataGridView1.Location = New Point(49, 197)
        titulo.TextAlign = HorizontalAlignment.Center
        titulo.Text = "Histórico de localizações"
        titulo2.TextAlign = HorizontalAlignment.Center
        titulo2.Text = "Protótipo: " & table2.Rows(0)(1) & ", Código Tag: " & table2.Rows(0)(0) & ", ID: " & table2.Rows(0)(2)
        titulo2.Visible = True
        PictureBox22.Visible = True

        PanelLogin.Visible = False
        PanelRegisto.Visible = False
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelMostrarProt.Visible = True
        PanelRemove.Visible = False


    End Sub
	
	Private Sub seta_click(sender As Object, e As EventArgs) Handles PictureBox22.Click

        PanelLogin.Visible = False
        PanelRegisto.Visible = False
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = True
        PanelMostrarProt.Visible = False
        PanelRemove.Visible = False

    End Sub
	
	Private Sub Remove_prot(sender As Object, e As EventArgs) Handles btnConfirmRemove.Click

        If TextBoxRemoveTag.Text = "" Or TextBoxRemoveID.Text = "" Then

            MessageBox.Show("Por favor preencha os campos Código Tag e ID!")
        Else

            Dim command10 As New MySqlCommand("SELECT * FROM prototype where tag_code = @cod_tag and prototype_id = @id_proto", connection)
            command10.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = TextBoxRemoveTag.Text
            command10.Parameters.Add("@id_proto", MySqlDbType.VarChar).Value = TextBoxRemoveID.Text

            Dim adapter10 As New MySqlDataAdapter(command10)
            Dim table10 As New DataTable()
            adapter10.Fill(table10)

            If table10.Rows.Count = 0 Then

                MessageBox.Show("Nenhum protótipo encontrado com o Código Tag e ID introduzidos!")

            Else

                Dim command6 As New MySqlCommand("SELECT * FROM prototype where tag_code = @cod_tag", connection)
                command6.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = TextBoxRemoveTag.Text

                Dim adapter6 As New MySqlDataAdapter(command6)
                Dim table6 As New DataTable()
                adapter6.Fill(table6)

                Dim command5 As New MySqlCommand("SELECT * FROM prototype where prototype_id = @prot_id", connection)
                command5.Parameters.Add("@prot_id", MySqlDbType.VarChar).Value = TextBoxRemoveID.Text

                Dim adapter As New MySqlDataAdapter(command5)
                Dim table As New DataTable()
                adapter.Fill(table)

                Dim Codigo_Tag As String
                Dim Codigo_Tag2 As String

                If table.Rows.Count = 0 And table6.Rows.Count = 0 Then
                    MessageBox.Show("Código Tag / ID já removidos")
                    TextBoxRemoveID.Text = ""
                    TextBoxRemoveTag.Text = ""
                Else
                    If table.Rows.Count = 0 Then
                        Codigo_Tag2 = table6.Rows(0)(0).ToString


                        Dim Command As New MySqlCommand("DELETE FROM prototype where tag_code = @tag_cod", connection)

                        Command.Parameters.Add("@tag_cod", MySqlDbType.VarChar).Value = TextBoxRemoveTag.Text

                        connection.Open()
                        If Command.ExecuteNonQuery() Then
                            MessageBox.Show("Protótipo apagado com sucesso!")
                        End If
                        connection.Close()

                        Dim command3 As New MySqlCommand("DELETE FROM history where tag_code = @tag_id2", connection)

                        command3.Parameters.Add("@tag_id2", MySqlDbType.VarChar).Value = Codigo_Tag2

                        connection.Open()
                        If command3.ExecuteNonQuery() Then
                            TextBoxRemoveID.Text = ""
                            TextBoxRemoveTag.Text = ""
                        End If
                        connection.Close()

                    Else
                        Codigo_Tag = table.Rows(0)(0).ToString

                        Dim command2 As New MySqlCommand("DELETE FROM prototype where prototype_id = @id", connection)

                        command2.Parameters.Add("@id", MySqlDbType.VarChar).Value = TextBoxRemoveID.Text

                        connection.Open()
                        If command2.ExecuteNonQuery() Then
                            MessageBox.Show("Protótipo apagado com sucesso!")
                        End If
                        connection.Close()

                        Dim command4 As New MySqlCommand("DELETE FROM history where tag_code = @tag_id", connection)

                        command4.Parameters.Add("@tag_id", MySqlDbType.VarChar).Value = Codigo_Tag

                        connection.Open()
                        If command4.ExecuteNonQuery() Then
                            TextBoxRemoveID.Text = ""
                            TextBoxRemoveTag.Text = ""
                        End If
                        connection.Close()


                    End If
                End If
            End If

        End If
    End Sub
	
End Class
	
	
	
	