Module Module1

    Sub Main()
        ' Very Basic app to echo back the standard input to standard error / out

        While True
            Dim input As String = Console.In.ReadLine()
            Console.Out.WriteLine("STDOUT: " & input)
            Console.Error.WriteLine("STDERR: " & input)
        End While

    End Sub

End Module
