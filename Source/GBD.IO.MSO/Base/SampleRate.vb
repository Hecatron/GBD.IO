
Imports System.Runtime.CompilerServices

Namespace Base

#Region "Public Types"

    ''' <summary>
    ''' Available Sample Rates
    ''' </summary>
    Public Enum SampleRate
        Rate2G = 2000000000
        Rate1G = 1000000000
        Rate200M = 200000000
        Rate100M = 100000000
        Rate50M = 50000000
        Rate20M = 20000000
        Rate10M = 10000000
        Rate5M = 5000000
        Rate2M = 2000000
        Rate1M = 1000000
        Rate500K = 500000
        Rate200K = 200000
        Rate100K = 100000
        Rate50K = 50000
        Rate20K = 20000
        Rate10K = 10000
        Rate5K = 5000
        Rate2K = 2000
        Rate1K = 1000
        Rate500 = 500
        Rate200 = 200
        Rate100 = 100
    End Enum

#End Region

#Region "Public Extension Methods"

    ''' <summary>
    ''' Extension Methods for Sample Rate Enum
    ''' </summary>
    Public Module SampleRateExtensions

        ''' <summary>
        ''' Return Formatted Sample Rate Per Second
        ''' </summary>
        <Extension()>
        Public Function SaFormatted(val As SampleRate) As String
            Dim ret As String = ""
            If val < 1000 Then
                ret = String.Format("{0:F0}", val & " Sa/s")
            ElseIf (val < 1000000) Then
                ret = String.Format("{0:F0}", (val / 1000) & " KSa/s")
            ElseIf (val < 1000000000) Then
                ret = String.Format("{0:F0}", (val / 1000000) & " MSa/s")
            ElseIf (val >= 1000000000) Then
                ret = String.Format("{0:F0}", (val / 1000000000) & " GSa/s")
            End If
            Return ret
        End Function

        ''' <summary>
        ''' Return the period of time for a division on a chart
        ''' Default assumes 100 samples per division
        ''' </summary>
        <Extension()>
        Public Function SecDivision(val As SampleRate, Optional SamplesPerDivision As Integer = 100) As Double
            Return SamplesPerDivision / val
        End Function

        ''' <summary>
        ''' Return the period of time for a division on a chart
        ''' Default assumes 100 samples per division - Formatted
        ''' </summary>
        <Extension()>
        Public Function SecDivFormatted(val As SampleRate, Optional SamplesPerDivision As Integer = 100) As String
            Dim tmp1 As Double = SamplesPerDivision / val
            Dim ret As String = tmp1 & "s/div"
            If tmp1 < 1 Then ret = (tmp1 * 1000) & "ms/div"
            If tmp1 < 0.001 Then ret = (tmp1 * 1000000) & "us/div"
            If tmp1 < 0.000001 Then ret = (tmp1 * 1000000000) & "ns/div"
            Return ret
        End Function

        ' TODO RIS

    End Module

#End Region

End Namespace
