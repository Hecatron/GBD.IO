Imports GBD.IO.Reactive.Stream

Namespace Probes

    ''' <summary> A base class for Bus Pirate probes. </summary>
    Public MustInherit Class BaseProbe

#Region "Properties"

        ''' <summary> Represents the associated Bus Pirate Device. </summary>
        ''' <value> The Bus Pirate Device. </value>
        Public ReadOnly Property Device As BPDevice
            Get
                Return _BPDevice
            End Get
        End Property
        Protected Property _BPDevice As BPDevice

        Protected Property ReadCache As RxMemStream

#End Region

#Region "Constructors"

        ''' <summary> Default constructor. </summary>
        Public Sub New(device As BPDevice)
            _BPDevice = device
        End Sub

#End Region

    End Class

End Namespace
