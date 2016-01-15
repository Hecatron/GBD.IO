
Namespace Base

    ''' <summary>
    ''' Base Class for MSO Device
    ''' </summary>
    Public Class MSOBaseDevice

#Region "Public Properties"

        ''' <summary>
        ''' Number of Sample Rates
        ''' </summary>
        Public Property NumberSampleRates

        ''' <summary>
        ''' Available Sample Rates
        ''' </summary>
        Public ReadOnly Property SampleRates As IEnumerable(Of SampleRate)
            Get
                Return _SampleRates
            End Get
        End Property
        Protected Property _SampleRates As List(Of SampleRate)

#End Region

#Region "Public Constructors"

        ''' <summary>
        ''' Default Constructor
        ''' </summary>
        Public Sub New()
            NumberSampleRates = 16
            _SampleRates.Clear()
            _SampleRates.Add(SampleRate.Rate100)




            'frmDSO.Disp.RateList[15] = 2000000000;
            'frmDSO.Disp.RateList[14] = 1000000000;
            'frmDSO.Disp.RateList[13] = 200000000;
            'frmDSO.Disp.RateList[12] = 100000000;
            'frmDSO.Disp.RateList[11] = 50000000;
            'frmDSO.Disp.RateList[10] = 20000000;
            'frmDSO.Disp.RateList[9] = 10000000;
            'frmDSO.Disp.RateList[8] = 5000000;
            'frmDSO.Disp.RateList[7] = 2000000;
            'frmDSO.Disp.RateList[6] = 1000000;
            'frmDSO.Disp.RateList[5] = 500000;
            'frmDSO.Disp.RateList[4] = 200000;
            'frmDSO.Disp.RateList[3] = 100000;
            'frmDSO.Disp.RateList[2] = 50000;
            'frmDSO.Disp.RateList[1] = 20000;
            'frmDSO.Disp.RateList[0] = 1000;
        End Sub

#End Region

    End Class

End Namespace
